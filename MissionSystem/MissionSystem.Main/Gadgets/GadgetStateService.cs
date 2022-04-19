using System.Net.NetworkInformation;
using MissionSystem.Main.MQTT;

namespace MissionSystem.Main.Gadgets;

public class GadgetStateService : IGadgetStateService
{
    private readonly IMqttClientService _mqtt;

    private readonly Dictionary<PhysicalAddress, Dictionary<string, object>> _states = new();

    private readonly Dictionary<PhysicalAddress, HashSet<IGadgetStateService.StateCallback>> _callbacks =
        new();

    public GadgetStateService(IMqttClientService mqtt)
    {
        _mqtt = mqtt;

        // TODO: get devices from GadgetService, subscribe to all
        // TODO: also subscribe to updates
        SubscribeToDevice(PhysicalAddress.Parse("44:17:93:87:D3:DC"));
    }

    public Unsubscribable StateUpdatesOf(PhysicalAddress device, IGadgetStateService.StateCallback callback)
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

        return new Unsubscribable(_callbacks[device], callback);
    }

    private void SubscribeToDevice(PhysicalAddress address)
    {
        _mqtt.SubscribeAsync($"gadgets/{formatMac(address)}/state", message => HandleMessage(address, message));
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

    private static string formatMac(PhysicalAddress addr)
    {
        var bytes = addr.GetAddressBytes();
        return string.Join(":", bytes.Select(z => z.ToString("X2")).ToArray());
    }
}
