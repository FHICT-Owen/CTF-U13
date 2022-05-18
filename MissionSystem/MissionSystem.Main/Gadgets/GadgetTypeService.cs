using Microsoft.EntityFrameworkCore;
using MissionSystem.Data;
using MissionSystem.Interface.Models;
using MissionSystem.Interface.Services;

namespace MissionSystem.Main.Gadgets;

public class GadgetTypeService : IGadgetTypeService
{
    public async Task<List<GadgetType>> GetGadgetTypesAsync()
    {
        await using var db = new DataStore();
        return await db.GadgetTypes.ToListAsync();
    }
}
