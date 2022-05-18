using Microsoft.EntityFrameworkCore;
using MissionSystem.Data;
using MissionSystem.Interface.Models;
using MissionSystem.Interface.Services;
using MissionSystem.Util;

namespace MissionSystem.Main.Arenas;

public class ArenaService : SubscribableResource<Arena>, IArenaService
{
    public async Task<List<Arena>> GetArenasAsync()
    {
        await using var db = new DataStore();
        return await db.Arenas.ToListAsync();
    }


    public async Task<Arena?> FindArenaAsync(int id)
    {
        await using var db = new DataStore();
        var arena = await db.Arenas.FindAsync(id);

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
