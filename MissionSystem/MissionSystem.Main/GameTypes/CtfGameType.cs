using MissionSystem.Data.Models;

namespace MissionSystem.Main.GameTypes;

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
