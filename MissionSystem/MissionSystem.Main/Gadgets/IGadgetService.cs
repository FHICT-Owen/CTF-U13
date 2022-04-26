using MissionSystem.Data.Models;
using MissionSystem.Util;

namespace MissionSystem.Main.Gadgets;

/// <summary>
/// Service which can be used to manage gadgets
/// </summary>
public interface IGadgetService : ISubscribableResource<Gadget>
{
    /// <summary>
    /// Gets a list of all gadgets currently known to the system
    /// </summary>
    /// <returns>All gadgets currently known to the system</returns>
    public Task<List<Gadget>> GetGadgets();

    /// <summary>
    /// Adds a new gadget to the system
    /// </summary>
    /// <param name="gadget">Gadget to add</param>
    public Task AddGadget(Gadget gadget);

    /// <summary>
    /// Removes a gadget from the system
    /// </summary>
    /// <param name="gadget">Gadget to remove</param>
    public Task DeleteGadget(Gadget gadget);
}
