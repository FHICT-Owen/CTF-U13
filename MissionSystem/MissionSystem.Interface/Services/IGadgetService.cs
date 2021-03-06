using System.Net.NetworkInformation;
using MissionSystem.Interface.Models;

namespace MissionSystem.Interface.Services;

/// <summary>
/// Service which can be used to manage gadgets
/// </summary>
public interface IGadgetService : ISubscribableResource<Gadget>
{
    /// <summary>
    /// Gets a list of all gadgets currently known to the system
    /// </summary>
    /// <returns>All gadgets currently known to the system</returns>
    public Task<List<Gadget>> GetGadgetsAsync();
    /// <summary>
    /// Adds a new gadget to the system
    /// </summary>
    /// <param name="gadget">Gadget to add</param>
    public Task AddGadgetAsync(Gadget gadget);

    /// <summary>
    /// Removes a gadget from the system
    /// </summary>
    /// <param name="gadget">Gadget to remove</param>
    public Task DeleteGadgetAsync(Gadget gadget);

    /// <summary>
    /// Gets a gadget from the system
    /// </summary>
    /// <param name="id">The ID of the gadget to retrieve</param>
    /// <returns></returns>
    public Task<Gadget?> FindGadgetAsync(PhysicalAddress id);

    /// <summary>
    /// Update a gadget in the system
    /// </summary>
    /// <param name="gadget">Gadget to update</param>
    /// <returns></returns>
    public Task UpdateGadgetAsync(Gadget gadget);
}
