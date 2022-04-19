using System.Net.NetworkInformation;

namespace MissionSystem.Main.Gadgets;

public interface IGadgetStateService
{
    delegate void StateCallback(Dictionary<string, object> state);

    public Unsubscribable StateUpdatesOf(PhysicalAddress device, StateCallback callback);
}
