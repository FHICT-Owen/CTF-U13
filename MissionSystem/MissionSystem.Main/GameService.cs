using MissionSystem.Factory;
using MissionSystem.Interface;
using MissionSystem.Interface.Models;
using MissionSystem.Interface.Services;
using MissionSystem.Util;

namespace MissionSystem.Main;
public class GameService : SubscribableResource<Match>, IGameService
{
    private IServiceProvider serviceProvider;
    private IArenaService arenaService;
    
    private List<Match> games = new();

    public GameService(IServiceProvider provider)
    {
        serviceProvider = provider;
        arenaService = serviceProvider.GetService<IArenaService>();
        arenaService.Deleted += DeleteGames;
    }
    public IBaseGame GetGame(string game, Arena arena)
    {
        int key = arena.Game?.Id ?? -1;
        // if (type == "ctf") return GameFactory.GetBaseGame(serviceProvider);
        if (!games.Any(x => x.Id == key))
        {
            return CreateGame(game, arena);
        }

        return games.Find(game => game.Id == key).BaseGame;
    }

    public IBaseGame CreateGame(string game, Arena arena)
    {
        int key = arena.Game?.Id ?? -1;
        if (!games.Any(x => x.Id == key))
        {
            games.Add(new Match
            {
                Id = key,
                GameTypeName = game,
                Arena = arena,
                BaseGame = GameFactory.GetBaseGame(serviceProvider, arena)
            });

            games.Find(game => game.Id == key).BaseGame.Setup();
        }

        return games.Find(game => game.Id == key).BaseGame;

    }

    public void DeleteGame(string game, Arena arena)
    {
        games.RemoveAll(x => x.GameTypeName == game && x.Arena.Id == arena.Id);
    }

    public void DeleteGames(string game)
    {
        games.RemoveAll(x => x.GameTypeName == game);
    }

    public void DeleteGames(Arena arena)
    {
        List<Match> deleteGames = games.FindAll(x => x.Arena.Id == arena.Id);
        foreach (Match game in deleteGames)
        {
            game.BaseGame.Dispose();
            games.Remove(game);

            OnDeleted(game);
        }
    }
}
