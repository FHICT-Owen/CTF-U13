using MissionSystem.Interface;

namespace MissionSystem.Main.GameTypes;

public class GameType : IGameType
{
    public string Name { get; }
    public int DefaultDuration { get; }

    public GameType(string name, int defaultDuration)
    {
        Name = name;
        DefaultDuration = defaultDuration;
    }
}
