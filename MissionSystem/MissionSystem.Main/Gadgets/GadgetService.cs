using Microsoft.EntityFrameworkCore;
using MissionSystem.Data;
using MissionSystem.Data.Models;
using MissionSystem.Util;

namespace MissionSystem.Main.Gadgets;

/// <summary>
/// The gadget service is responsible for managing saved gadgets
/// Because gadgets can be added/removed while the system is running, it can
/// also notify other services when this is done so they can update. themselves
/// </summary>
public class GadgetService : SubscribableResourceResource<Gadget>, IGadgetService
{
    public async Task<List<Gadget>> GetGadgets()
    {
        await using var db = new DataStore();
        return await db.Gadgets.ToListAsync();
    }

    public async Task AddGadget(Gadget gadget)
    {
        await using var db = new DataStore();
        await db.Gadgets.AddAsync(gadget);
        await db.SaveChangesAsync();

        OnAdded(gadget);
    }

    public async Task DeleteGadget(Gadget gadget)
    {
        await using var db = new DataStore();
        db.Gadgets.Remove(gadget);
        await db.SaveChangesAsync();

        OnDeleted(gadget);
    }
}
