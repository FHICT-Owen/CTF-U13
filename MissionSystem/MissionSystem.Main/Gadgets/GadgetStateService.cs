using System.Net.NetworkInformation;

namespace MissionSystem.Main.Gadgets;

public class GadgetStateService : IGadgetStateService
{
    private Dictionary<PhysicalAddress, IGadgetState> _states = new();

    public void StateUpdatesOf<T>(PhysicalAddress device, IGadgetStateService.StateCallback<T> callback)
        where T : IGadgetState
    {
        // device.ToString()
    }
}
