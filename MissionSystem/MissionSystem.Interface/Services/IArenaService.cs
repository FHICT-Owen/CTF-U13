using MissionSystem.Interface.Models;
using MissionSystem.Util;

namespace MissionSystem.Interface.Services;

/// <summary>
/// Service which can be used to manage arenas
/// </summary>
public interface IArenaService : ISubscribableResource<Arena>
{
    /// <summary>
    /// Gets a list of all arenas currently known to the system
    /// </summary>
    /// <returns>All arenas currently known to the system</returns>
    public Task<List<Arena>> GetArenasAsync();
    
    /// <summary>
    /// Adds a new arena to the system
    /// </summary>
    /// <param name="arena">Arena to add</param>
    public Task AddArenaAsync(Arena arena);

    /// <summary>
    /// Removes a arena from the system
    /// </summary>
    /// <param name="arena">Arena to remove</param>
    public Task DeleteArenaAsync(Arena arena);

    /// <summary>
    /// Gets a arena from the system
    /// </summary>
    /// <param name="id">The ID of the arena to retrieve</param>
    /// <returns></returns>
    public Task<Arena?> FindArenaAsync(int id);

    /// <summary>
    /// Update a arena in the system
    /// </summary>
    /// <param name="arena">Arena to update</param>
    /// <returns></returns>
    public Task UpdateArenaAsync(Arena arena);
}
