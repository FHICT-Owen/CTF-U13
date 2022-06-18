using System.Net.NetworkInformation;
using MissionSystem.Data.Repository;
using MissionSystem.Interface.Models;
using MissionSystem.Interface.Services;

namespace MissionSystem.Main.Gadgets;

/// <summary>
/// The gadget service is responsible for managing saved gadgets
/// Because gadgets can be added/removed while the system is running, it can
/// also notify other services when this is done so they can update. themselves
/// </summary>
public class GadgetService : DataService<Gadget, PhysicalAddress>, IGadgetService
{
    public GadgetService(Func<IRepository<Gadget, PhysicalAddress>> repoFactory) : base(repoFactory)
    {
    }

    public async Task<List<Gadget>> GetGadgetsAsync()
    {
        await using var repo = CreateRepo();
        return await repo.Get();
    }

    public async Task<Gadget?> FindGadgetAsync(PhysicalAddress id)
    {
        await using var repo = CreateRepo();
        return await repo.GetById(id);
    }

    public async Task UpdateGadgetAsync(Gadget gadget)
    {
        await using var repo = CreateRepo();
        repo.Update(gadget);
        await repo.Save();
    }

    public async Task AddGadgetAsync(Gadget gadget)
    {
        await using var repo = CreateRepo();
        await repo.Add(gadget);
        await repo.Save();

        OnAdded(gadget);
    }

    public async Task DeleteGadgetAsync(Gadget gadget)
    {
        await using var repo = CreateRepo();
        repo.Remove(gadget);
        await repo.Save();

        OnDeleted(gadget);
    }
}
