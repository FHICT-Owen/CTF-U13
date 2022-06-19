using MissionSystem.Data.Repository;
using MissionSystem.Interface.Models;
using MissionSystem.Interface.Services;

namespace MissionSystem.Main.Arenas;

public class ArenaService : DataService<Arena>, IArenaService
{
    private IGameService GameService;
    
    public ArenaService(IGameService gameService, Func<IRepository<Arena, int>> repoFactory) : base(repoFactory)
    {
        GameService = gameService;
    }

    public async Task<List<Arena>> GetArenasAsync()
    {
        await using var repo = CreateRepo();

        List<Arena> arenas = await repo.Get();
        
        List<Arena> arenasCopy = new List<Arena>();

        foreach (var arena in arenas)
        {
            Arena a = arena;
            Match match = await GameService.FindMatchById(arena.Id);

            a.Game = match;
            arenasCopy.Add(a);
        }

        return arenas;
    }


    public async Task<Arena?> FindArenaAsync(int id)
    {
        await using var repo = CreateRepo();
        var arena = await repo.GetById(id);

        Match match = await GameService.FindMatchById(id);

        arena.Game = match;

        return arena;
    }

    public async Task AddArenaAsync(Arena arena)
    {
        await using var repo = CreateRepo();

        await repo.Add(arena);
        await repo.Save();

        OnAdded(arena);
    }

    public async Task DeleteArenaAsync(Arena arena)
    {
        await using var repo = CreateRepo();

        repo.Remove(arena);
        await repo.Save();

        OnDeleted(arena);
    }

    public async Task UpdateArenaAsync(Arena arena)
    {
        await using var repo = CreateRepo();

        repo.Update(arena);

        await repo.Save();
    }
}
