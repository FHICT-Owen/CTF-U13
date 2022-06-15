using MissionSystem.Factory;
using MissionSystem.Interface;
using MissionSystem.Interface.Models;
using MissionSystem.Interface.Services;
using MissionSystem.Util;

namespace MissionSystem.Main;
public class GameService : SubscribableResource<Match>, IGameService
{
    private IServiceProvider serviceProvider;
    
    private Dictionary<int, Match> games = new();

    public GameService(IServiceProvider provider)
    {
        serviceProvider = provider;
    }
    public IBaseGame GetBaseGame(Match match)
    {
        if (!games.ContainsKey(match.Arena.Id))
        {
            return CreateGame(match);
        }

        return games[match.Arena.Id].BaseGame ?? CreateGame(match);
    }

    public async Task<Match> FindMatchById(int id)
    {
        if (!games.ContainsKey(id)) return null;
        Match match = games[id];

        match.BaseGame = GetBaseGame(match);

        return match;
    }

    public IBaseGame CreateGame(Match match)
    {
        int key = match.Arena.Id;

        if (!games.ContainsKey(match.Arena.Id) || games[key].BaseGame == null)
        {
            match.BaseGame = GameFactory.GetBaseGame(serviceProvider, match.Arena, match.Duration);
            games[match.Arena.Id] = match;

        }

        return games[key].BaseGame;

    }

    public async Task CreateMatchAsync(Match match)
    {
        CreateGame(match);
    }

    public async Task DeleteMatchAsync(Match match)
    {
        games.Remove(match.Arena.Id);
    }

    public void DeleteMatchesByArenaAsync(Arena arena)
    {
        games.Remove(arena.Id);
    }
}
