using MissionSystem.Data.Repository;

namespace MissionSystem.Main;

public abstract class DataService<TEntity, TKey> : SubscribableResource<TEntity>
    where TEntity : class
{
    protected readonly Func<IRepository<TEntity, TKey>> CreateRepo;

    protected DataService(Func<IRepository<TEntity, TKey>> repoFactory)
    {
        CreateRepo = repoFactory;
    }
}

public abstract class DataService<TEntity> : DataService<TEntity, int>
    where TEntity : class
{
    protected DataService(Func<IRepository<TEntity, int>> repoFactory) : base(repoFactory)
    {
    }
}
