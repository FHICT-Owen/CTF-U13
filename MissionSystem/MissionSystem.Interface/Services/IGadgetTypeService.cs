using MissionSystem.Data.Models;

namespace MissionSystem.Interface.Services;

/// <summary>
/// Service which can be used to manage gadget types
/// </summary>
public interface IGadgetTypeService
{
    /// <summary>
    /// Gets a list of all gadget types currently known to the system
    /// </summary>
    /// <returns>All gadget types currently known to the system</returns>
    public Task<List<GadgetType>> GetGadgetTypesAsync();
    
}
