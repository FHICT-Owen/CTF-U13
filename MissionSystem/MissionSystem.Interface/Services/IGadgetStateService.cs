using System.Net.NetworkInformation;
using MissionSystem.Interface.MQTT;

namespace MissionSystem.Interface.Services;

/// <summary>
/// Service which keeps track of the state of gadgets
/// </summary>
public interface IGadgetStateService : IDisposable
{
    /// <summary>
    /// Callback with new gadget state
    /// </summary>
    delegate void StateCallback(Dictionary<string, object> state);

    /// <summary>
    /// Subscribes to updates of a certain gadget
    /// </summary>
    /// <param name="device">The gadget to subscribe to.</param>
    /// <param name="callback">Callback called once with the current state and every time it updates.</param>
    /// <returns></returns>
    public IUnsubscribable StateUpdatesOf(PhysicalAddress device, StateCallback callback);
}
