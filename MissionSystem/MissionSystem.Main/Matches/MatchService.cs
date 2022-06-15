//using Microsoft.EntityFrameworkCore;
//using MissionSystem.Data;
//using MissionSystem.Interface.Models;
//using MissionSystem.Interface.Services;
//using MissionSystem.Util;

//namespace MissionSystem.Main.Matches;
//public class MatchService : SubscribableResource<Match>, IMatchService
//{
//    public async Task CreateMatchAsync(Match match)
//    {
//        await using var db = new DataStore();

//        List<Gadget> gadgets = new List<Gadget>();

//        foreach (Gadget gadget in match.Gadgets)
//        {
//            gadgets.Add(await db.Gadgets.FindAsync(gadget.MacAddress));
//        }

//        match.Gadgets = gadgets;

//        Arena a = await db.Arenas.FindAsync(match.Arena.Id);

//        match.Arena = a;

//        await db.Matches.AddAsync(match);
//        db.SaveChanges();

//        OnAdded(match);
//    }
//    public async Task DeleteMatchAsync(Match match)
//    {
//        await using var db = new DataStore();
//        db.Matches.Remove(match);
//        await db.SaveChangesAsync();

//        OnDeleted(match);
//    }

//    public async Task UpdateMatchAsync(Match match)
//    {
//        await using var db = new DataStore();
//        db.Update(match);
//        await db.SaveChangesAsync();
//    }

//    public async Task<Match> FindMatchAsync(string id)
//    {
//        await using var db = new DataStore();
//        var match = await db.Matches.FindAsync(id);

//        return match;
//    }

//    public async Task<List<Match>> GetMatchesAsync()
//    {
//        await using var db = new DataStore();
//        return await db.Matches.ToListAsync();
//    }
//}
