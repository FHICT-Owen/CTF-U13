using System.Net.NetworkInformation;
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

    public async Task UpdateGadgetAsync(PhysicalAddress id, IGadgetService.UpdateGadgetCallback callback)
    {
        await using var db = new DataStore();
        var gadget = await db.Gadgets.FindAsync(id);

        // TODO: exception?
        if (gadget == null)
        {
            return;
        }
        
        await callback(gadget);
        await db.SaveChangesAsync();
    }

    public async Task AddGadgetAsync(Gadget gadget)
    {
        await using var db = new DataStore();
        await db.Gadgets.AddAsync(gadget);
        await db.SaveChangesAsync();

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
