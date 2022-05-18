using System.Net.NetworkInformation;

namespace MissionSystem.Interface.Services;

public interface IGadgetSettingsService
{
    /// <summary>
    /// Updates a gadget's setting
    /// </summary>
    /// <param name="device">The gadget to update</param>
    /// <param name="key">The key of the setting to update</param>
    /// <param name="value">The value of the setting to update</param>
    public Task SetSetting(PhysicalAddress device, string key, object value);

    /// <summary>
    /// Updates multiple gadget settings at once
    /// </summary>
    /// <param name="device">The gadget to update</param>
    /// <param name="opts">The key/value pairs of the settings to update</param>
    public Task SetSettings(PhysicalAddress device, Dictionary<string, object> opts);
}
