namespace MissionSystem.Main.Gadgets;

public interface IFlagGadgetState : IGadgetState
{
    public Team CapturedBy { get; }

    public uint CaptureProgress { get; }

    public bool BeingCaptured { get; }
}
