namespace MissionSystem.Data.Repository;

public interface IRepository<TEntity, in TKey> : IDisposable, IAsyncDisposable
    where TEntity : class
{
    /// <summary>
    /// Get all entities stored in the repository.
    /// </summary>
    /// <returns>All entities stored.</returns>
    ValueTask<List<TEntity>> Get();
    
    /// <summary>
    /// Get an entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>The entity if found.</returns>
    ValueTask<TEntity?> GetById(TKey id);
    
    /// <summary>
    /// Add an entity to the repository.
    /// </summary>
    /// <param name="gadget">The entity to add.</param>
    Task Add(TEntity gadget);
    
    /// <summary>
    /// Update the entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(TEntity entity);
    
    /// <summary>
    /// Remove an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    void Remove(TEntity entity);
    
    /// <summary>
    /// Save all pending changes to the datastore.
    /// </summary>
    Task Save();
}
