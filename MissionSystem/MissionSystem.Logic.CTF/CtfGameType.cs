using MissionSystem.Interface.Models;

namespace MissionSystem.Logic.CTF;

public class CtfGameType : GameType
{
    public CtfGameType() : base("Capture The Flags", 20)
    {
    }

    public override bool CanUseGadgetType(GadgetType type)
    {
        return type.RefId == "flag";
    }
}
