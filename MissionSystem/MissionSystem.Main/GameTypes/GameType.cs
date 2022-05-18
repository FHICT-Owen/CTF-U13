using MissionSystem.Data.Models;
using MissionSystem.Interface;

namespace MissionSystem.Main.GameTypes;

public abstract class GameType : IGameType
{
    public string Name { get; }
    public int DefaultDuration { get; }

    protected GameType(string name, int defaultDuration)
    {
        Name = name;
        DefaultDuration = defaultDuration;
    }

    public abstract bool CanUseGadgetType(GadgetType type);
}
