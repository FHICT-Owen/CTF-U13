using Microsoft.EntityFrameworkCore;
using MissionSystem.Data;
using MissionSystem.Interface.Models;
using MissionSystem.Interface.Services;
using MissionSystem.Util;

namespace MissionSystem.Main.Arenas;

public class ArenaService : SubscribableResource<Arena>, IArenaService
{
    private IGameService GameService;

    public ArenaService(IGameService gameService)
    {
        GameService = gameService;
    }

    public async Task<List<Arena>> GetArenasAsync()
    {
        await using var db = new DataStore();

        List<Arena> arenas = await db.Arenas.ToListAsync();

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
        await using var db = new DataStore();
        var arena = await db.Arenas.FindAsync(id);

        Match match = await GameService.FindMatchById(id);

        arena.Game = match;

        return arena;
    }

    public async Task AddArenaAsync(Arena arena)
    {
        await using var db = new DataStore();

        await db.Arenas.AddAsync(arena);
        await db.SaveChangesAsync();

        OnAdded(arena);
    }

    public async Task DeleteArenaAsync(Arena arena)
    {
        await using var db = new DataStore();

        db.Arenas.Remove(arena);
        await db.SaveChangesAsync();

        OnDeleted(arena);
    }

    public async Task UpdateArenaAsync(Arena arena)
    {
        await using var db = new DataStore();

        db.Update(arena);

        await db.SaveChangesAsync();
    }
}
