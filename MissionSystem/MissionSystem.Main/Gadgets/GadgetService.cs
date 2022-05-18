using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using MissionSystem.Data;
using MissionSystem.Data.Models;
using MissionSystem.Interface.Services;
using MissionSystem.Util;

namespace MissionSystem.Main.Gadgets;

/// <summary>
/// The gadget service is responsible for managing saved gadgets
/// Because gadgets can be added/removed while the system is running, it can
/// also notify other services when this is done so they can update. themselves
/// </summary>
public class GadgetService : SubscribableResource<Gadget>, IGadgetService
{
    public async Task<List<Gadget>> GetGadgetsAsync()
    {
        await using var db = new DataStore();
        return await db.Gadgets.ToListAsync();
    }

    public async Task<Gadget?> FindGadgetAsync(PhysicalAddress id)
    {
        await using var db = new DataStore();
        var gadget = await db.Gadgets.FindAsync(id);

        return gadget;
    }

    public async Task UpdateGadgetAsync(Gadget gadget)
    {
        await using var db = new DataStore();
        db.Update(gadget);
        await db.SaveChangesAsync();
    }

    public async Task AddGadgetAsync(Gadget gadget)
    {
        await using var db = new DataStore();
        var entry = await db.Gadgets.AddAsync(gadget);
        await db.SaveChangesAsync();

        // Load type of this gadget
        await entry.Reference(g => g.Type).LoadAsync();

        OnAdded(gadget);
    }

    public async Task DeleteGadgetAsync(Gadget gadget)
    {
        await using var db = new DataStore();
        db.Gadgets.Remove(gadget);
        await db.SaveChangesAsync();

        OnDeleted(gadget);
    }
}
