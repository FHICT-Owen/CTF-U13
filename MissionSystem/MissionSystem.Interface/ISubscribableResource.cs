namespace MissionSystem.Interface;

/// <summary>
/// A class which can be used to subscribe to a resource
/// </summary>
/// <typeparam name="T">The type of resource one can subscribe to</typeparam>
public interface ISubscribableResource<out T>
{
    public event Action<T> Added;
    public event Action<T> Deleted;
}
