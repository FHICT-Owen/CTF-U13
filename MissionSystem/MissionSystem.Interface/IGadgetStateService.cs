using System.Net.NetworkInformation;

namespace MissionSystem.Interface;

public interface IGadgetStateService
{
    delegate void StateCallback(Dictionary<string, object> state);

    public void Subscribe(string device);
    public IUnsubscribable StateUpdatesOf(PhysicalAddress device, StateCallback callback);

}
