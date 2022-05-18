using MissionSystem.Interface;
using MissionSystem.Interface.Services;

namespace MissionSystem.Main.GameTypes;

public class GameTypeService : IGameTypeService
{
    private readonly Dictionary<string, IGameType> _gameTypes = new()
    {
        // TODO: register dynamically
        {"ctf", new CtfGameType()}
    };

    public Dictionary<string, IGameType> GameTypes => _gameTypes;
}
