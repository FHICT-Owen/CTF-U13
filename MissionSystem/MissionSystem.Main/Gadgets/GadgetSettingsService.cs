using System.Net.NetworkInformation;
using MissionSystem.Interface.Services;
using MissionSystem.Main.MQTT;
using MissionSystem.Util;

namespace MissionSystem.Main.Gadgets;

public class GadgetSettingsService : IGadgetSettingsService
{
    /// <summary>
    /// MQTT Client service used to interact with the MQTT broker
    /// </summary>
    private readonly IMqttClientService _mqtt;

    private readonly Dictionary<PhysicalAddress, Dictionary<string, object>> _settings = new();

    public GadgetSettingsService(IMqttClientService mqtt)
    {
        _mqtt = mqtt;
    }

    public async Task SetSetting(PhysicalAddress device, string key, object value)
    {
        if (!_settings.ContainsKey(device))
        {
            _settings[device] = new Dictionary<string, object>();
        }

        _settings[device][key] = value;

        await UpdateSettings(device);
    }
    
    public async Task SetSettings(PhysicalAddress device, Dictionary<string, object> opts)
    {
        if (!_settings.ContainsKey(device))
        {
            _settings[device] = new Dictionary<string, object>();
        }
        
        foreach (var (key, value) in opts)
        {
            _settings[device][key] = value;
        }

        await UpdateSettings(device);
    }

    private async Task UpdateSettings(PhysicalAddress device)
    {
        if (!_settings.ContainsKey(device))
        {
            return;
        }

        await _mqtt.SendMessageAsync($"gadgets/{device.ToFormattedString()}/settings", _settings[device]);
    }
}
