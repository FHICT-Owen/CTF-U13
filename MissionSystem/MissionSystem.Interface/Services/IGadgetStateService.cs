using System.Net.NetworkInformation;
using MissionSystem.Interface.MQTT;

namespace MissionSystem.Interface.Services;

public interface IGadgetStateService
{
    delegate void StateCallback(Dictionary<string, object> state);

    public void Subscribe(string device);
    public IUnsubscribable StateUpdatesOf(PhysicalAddress device, StateCallback callback);

}
