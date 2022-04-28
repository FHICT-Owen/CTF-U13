using MissionSystem.Factory;
using MissionSystem.Interface;
using MissionSystem.Interface.Services;

namespace MissionSystem.Main;
public class GameService : IGameService
{
    private IServiceProvider serviceProvider;
    
    private Dictionary<string, IBaseGame> games = new();

    public GameService(IServiceProvider provider)
    {
        serviceProvider = provider;
    }
    public IBaseGame GetGame(string key)
    {

        // if (type == "ctf") return GameFactory.GetBaseGame(serviceProvider);
        if (!games.ContainsKey(key))
        {
            games[key] = GameFactory.GetBaseGame(serviceProvider);
            games[key].Setup();
        }

        return games[key];
    }
}
