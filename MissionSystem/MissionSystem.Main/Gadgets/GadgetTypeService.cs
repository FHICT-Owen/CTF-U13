using MissionSystem.Data.Repository;
using MissionSystem.Interface.Models;
using MissionSystem.Interface.Services;

namespace MissionSystem.Main.Gadgets;

public class GadgetTypeService : DataService<GadgetType>, IGadgetTypeService
{
    public GadgetTypeService(Func<Repository<GadgetType, int>> storeFactory) : base(storeFactory)
    {
    }

    public async Task<List<GadgetType>> GetGadgetTypesAsync()
    {
        await using var db = CreateRepo();
        return await db.Get();
    }
}
