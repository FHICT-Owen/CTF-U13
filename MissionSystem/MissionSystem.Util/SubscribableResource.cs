namespace MissionSystem.Util;

public abstract class SubscribableResource<T> : ISubscribableResource<T>
{
    public event Action<T>? Added;
    public event Action<T>? Deleted;

    protected void OnAdded(T resource)
    {
        Added?.Invoke(resource);
    }

    protected void OnDeleted(T resource)
    {
        Deleted?.Invoke(resource);
    }
}
