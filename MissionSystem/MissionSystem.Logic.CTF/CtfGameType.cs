using MissionSystem.Interface.Models;

namespace MissionSystem.Logic.CTF;

public class CtfGameType : GameType
{
    public int FlagCount { get; }

    public CtfGameType(string name, int flagCount) : base(name, 20)
    {
        FlagCount = flagCount;
    }

    public override bool CanUseGadgetType(GadgetType type)
    {
        return type.RefId == "flag";
    }
}
