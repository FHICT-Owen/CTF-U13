using System.Net.NetworkInformation;

namespace MissionSystem.Main.Gadgets;

public interface IGadgetStateService
{

    delegate void StateCallback<in T>(T state) where T : IGadgetState;
    
    public void StateUpdatesOf<T>(PhysicalAddress device, StateCallback<T> callback) where T: IGadgetState;
}
