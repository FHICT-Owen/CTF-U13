using MissionSystem.Interface;
using MissionSystem.Interface.Models;

namespace MissionSystem.Logic;

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
