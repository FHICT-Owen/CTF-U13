using System.Net.NetworkInformation;
using MissionSystem.Interface.Services;
using MissionSystem.Main.MQTT;
using MissionSystem.Util;

namespace MissionSystem.Main.Gadgets;

public class GadgetStateService : IGadgetStateService
{
    /// <summary>
    /// MQTT Client service used to interact with the MQTT broker
    /// </summary>
    private readonly IMqttClientService _mqtt;

    /// <summary>
    /// Stores current known state of gadgets
    /// </summary>
    private readonly Dictionary<PhysicalAddress, Dictionary<string, object>> _states = new();

    /// <summary>
    /// Stores when the gadgets were last seen
    /// </summary>
    private readonly Dictionary<PhysicalAddress, DateTime> _lastSeen = new();

    /// <summary>
    /// MQTT subscription to gadget states
    /// </summary>
    private IUnsubscribable? _subscription;

    /// <summary>
    /// Stores all callbacks that should be called when a certain gadget's state changes
    /// </summary>
    private readonly Dictionary<PhysicalAddress, HashSet<IGadgetStateService.StateCallback>> _callbacks =
        new();

    public GadgetStateService(IMqttClientService mqtt)
    {
        _mqtt = mqtt;

        _ = SubscribeToAllGadgets();
    }

    public IUnsubscribable StateUpdatesOf(PhysicalAddress device, IGadgetStateService.StateCallback callback)
    {
        if (!_callbacks.ContainsKey(device))
        {
            _callbacks[device] = new HashSet<IGadgetStateService.StateCallback>();
        }

        _callbacks[device].Add(callback);

        if (_states.ContainsKey(device))
        {
            callback.Invoke(_lastSeen[device], _states[device]);
        }

        return new Unsubscribable<IGadgetStateService.StateCallback>(_callbacks[device], callback);
    }

    private async Task SubscribeToAllGadgets()
    {
        _subscription = await _mqtt.SubscribeAsync(
            $"gadgets/+/state",
            (topic, message) =>
            {
                var topicParts = topic.Split("/");
                HandleMessage(PhysicalAddress.Parse(topicParts[1]), message);
            }
        );
    }

    private void HandleMessage(PhysicalAddress address, Dictionary<string, object> msg)
    {
        _states[address] = msg;

        if (!_callbacks.ContainsKey(address)) return;

        _lastSeen[address] = DateTime.Now;

        foreach (var cb in _callbacks[address])
        {
            cb(_lastSeen[address], _states[address]);
        }
    }

    public void Dispose()
    {
        _subscription?.Dispose();
    }
}
