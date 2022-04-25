using System.Net.NetworkInformation;
using MissionSystem.Data.Models;
using MissionSystem.Interface;
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
    /// Gadget service used to get a list of all gadgets
    /// </summary>
    private readonly IGadgetService _gadgetService;

    /// <summary>
    /// Stores current known state of gadgets
    /// </summary>
    private readonly Dictionary<PhysicalAddress, Dictionary<string, object>> _states = new();

    /// <summary>
    /// Stores MQTT subscriptions to gadgets
    /// </summary>
    private readonly Dictionary<PhysicalAddress, IUnsubscribable> _gadgetSubscriptions = new();

    /// <summary>
    /// Stores all callbacks that should be called when a certain gadget's state changes
    /// </summary>
    private readonly Dictionary<PhysicalAddress, HashSet<IGadgetStateService.StateCallback>> _callbacks =
        new();

    public GadgetStateService(IMqttClientService mqtt, IGadgetService gadgetService)
    {
        _mqtt = mqtt;
        _gadgetService = gadgetService;

        _ = SubscribeToAllGadgets();

        // Get updates for future new gadgets / removals of gadgets
        _gadgetService.SubscribeToResource(OnGadgetAdded, OnGadgetRemoved);
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
            callback.Invoke(_states[device]);
        }

        return new Unsubscribable<IGadgetStateService.StateCallback>(_callbacks[device], callback);
    }

    private void OnGadgetAdded(Gadget gadget)
    {
        _ = SubscribeToDevice(gadget.MacAddress);
    }

    private void OnGadgetRemoved(Gadget gadget)
    {
        UnsubscribeDevice(gadget.MacAddress);
    }

    private async Task SubscribeToAllGadgets()
    {
        // Subscribe to all gadgets currently known to gadget service
        foreach (var gadget in await _gadgetService.GetGadgets())
        {
            // Subscribe to all gadgets
            await SubscribeToDevice(gadget.MacAddress);
        }
    }

    private async Task SubscribeToDevice(PhysicalAddress address)
    {
        var unsub = await _mqtt.SubscribeAsync(
            $"gadgets/{address.ToFormattedString()}/state",
            message => HandleMessage(address, message)
        );

        _gadgetSubscriptions.Add(address, unsub);
    }

    private void UnsubscribeDevice(PhysicalAddress address)
    {
        if (!_gadgetSubscriptions.ContainsKey(address)) return;

        _gadgetSubscriptions[address].Dispose();
        _gadgetSubscriptions.Remove(address);
    }

    private void HandleMessage(PhysicalAddress address, Dictionary<string, object> msg)
    {
        _states[address] = msg;

        if (!_callbacks.ContainsKey(address)) return;

        foreach (var cb in _callbacks[address])
        {
            cb(_states[address]);
        }
    }

    public void Dispose()
    {
        foreach (var (_, sub) in _gadgetSubscriptions)
        {
            sub.Dispose();
        }
    }
}
