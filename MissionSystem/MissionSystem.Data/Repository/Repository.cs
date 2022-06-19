using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MissionSystem.Data.Repository;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
{
    private readonly DataStore _store;
    private readonly DbSet<TEntity> _set;

    public Repository(DataStore store)
    {
        _store = store;
        _set = store.Set<TEntity>();
    }

    public virtual async ValueTask<List<TEntity>> Get()
    {
        return await _set.ToListAsync();
    }

    public virtual async ValueTask<TEntity?> GetById(TKey id)
    {
        return await _set.FindAsync(id);
    }

    public virtual async Task Add(TEntity gadget)
    {
        await AddEntry(gadget);
    }

    /// <summary>
    /// Creates an entry for the given entity
    ///
    /// </summary>
    /// <param name="entity">The entity to create an entry for</param>
    /// <returns>The created entry for the entity</returns>
    protected async ValueTask<EntityEntry<TEntity>> AddEntry(TEntity entity)
    {
        return await _set.AddAsync(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _set.Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        _set.Remove(entity);
    }

    public async Task Save()
    {
        await _store.SaveChangesAsync();
    }

    public void Dispose()
    {
        _store.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _store.DisposeAsync();
    }
}
