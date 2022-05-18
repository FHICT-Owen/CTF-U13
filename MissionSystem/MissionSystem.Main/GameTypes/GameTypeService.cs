using MissionSystem.Interface;
using MissionSystem.Interface.Services;

namespace MissionSystem.Main.GameTypes;

public class GameTypeService : IGameTypeService
{
    private readonly Dictionary<string, IGameType> _gameTypes = new();
    private readonly ILogger<GameTypeService> _logger;

    public Dictionary<string, IGameType> GameTypes => _gameTypes;

    public GameTypeService(ILogger<GameTypeService> logger)
    {
        _logger = logger;
    }

    public void RegisterGameType(string key, IGameType type)
    {
        if (_gameTypes.ContainsKey(key))
        {
            throw new Exception("gametype key already in use");
        }
        
        _logger.LogInformation(string.Format("Registered game type \"{0}\" ({1})", type.Name, key));

        _gameTypes[key] = type;
    }
}
