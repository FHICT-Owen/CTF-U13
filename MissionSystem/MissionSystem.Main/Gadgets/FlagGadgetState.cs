namespace MissionSystem.Main.Gadgets;

public struct FlagGadgetState : IFlagGadgetState
{
    public DateTime? LastSeen { get; } = null;

    public Team CapturedBy { get; private set; } = Team.NeutralTeam;

    public uint CaptureProgress { get; private set; } = 0;

    public bool BeingCaptured { get; private set; } = false;
}
