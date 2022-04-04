using System.Net.NetworkInformation;

namespace MissionSystem.Main.Gadgets;

public class TestGadgetUserService
{
    public TestGadgetUserService(IGadgetStateService gadgetStates)
    {
        gadgetStates.StateUpdatesOf<IFlagGadgetState>(PhysicalAddress.None, OnFlagUpdate);
    }

    private void OnFlagUpdate(IFlagGadgetState state)
    {
        
    }
}
