using System.Net.NetworkInformation;
using MissionSystem.Interface.Models;

namespace MissionSystem.Data.Repository;

public class GadgetRepository : Repository<Gadget, PhysicalAddress>
{
    public GadgetRepository(DataStore store) : base(store)
    {
    }

    /// <summary>
    /// Adds the given gadget to the repository.
    ///
    /// Also loads the <see cref="GadgetType"/> relation of this gadget.
    /// </summary>
    /// <param name="gadget">The gadget to add to the repository.</param>
    public override async Task Add(Gadget gadget)
    {
        var entry = await AddEntry(gadget);

        // Load GadgetType relation navigational property after adding it
        await entry.Reference(g => g.Type).LoadAsync();
    }
}
