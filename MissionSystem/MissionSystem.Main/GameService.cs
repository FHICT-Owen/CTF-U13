using MissionSystem.Factory;
using MissionSystem.Interface;
using MissionSystem.Interface.Models;
using MissionSystem.Interface.Services;
using MissionSystem.Util;
using System.Linq;
using static MissionSystem.Main.GameService;

namespace MissionSystem.Main;
public class GameService : SubscribableResource<Game>, IGameService
{
    private IServiceProvider serviceProvider;
    private IArenaService arenaService;
    
    private List<Game> games = new();

    public GameService(IServiceProvider provider)
    {
        serviceProvider = provider;
        arenaService = serviceProvider.GetService<IArenaService>();
        arenaService.Deleted += DeleteGames;
    }
    public IBaseGame GetGame(string game, Arena arena)
    {
        string key = $"{game}_{arena.Name}";
        // if (type == "ctf") return GameFactory.GetBaseGame(serviceProvider);
        if (!games.Any(x => x.Key == key))
        {
            return CreateGame(game, arena);
        }

        return games.Find(game => game.Key == key).BaseGame;
    }

    public IBaseGame CreateGame(string game, Arena arena)
    {
        string key = $"{game}_{arena.Name}";
        if (!games.Any(x => x.Key == key))
        {
            games.Add(new Game
            {
                Key = key,
                GameType = game,
                Arena = arena,
                BaseGame = GameFactory.GetBaseGame(serviceProvider)
            });

            games.Find(game => game.Key == key).BaseGame.Setup();
        }

        return games.Find(game => game.Key == key).BaseGame;

    }

    public void DeleteGame(string game, Arena arena)
    {
        games.RemoveAll(x => x.GameType == game && x.Arena.Id == arena.Id);
    }

    public void DeleteGames(string game)
    {
        games.RemoveAll(x => x.GameType == game);
    }

    public void DeleteGames(Arena arena)
    {
        List<Game> deleteGames = games.FindAll(x => x.Arena.Id == arena.Id);
        foreach (Game game in deleteGames)
        {
            game.BaseGame.Dispose();
            games.Remove(game);

            OnDeleted(game);
        }
    }
}
