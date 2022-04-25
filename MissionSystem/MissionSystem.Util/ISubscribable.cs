using MissionSystem.Interface;

namespace MissionSystem.Util;

/// <summary>
/// A class which can be used to subscribe to a resource
/// </summary>
/// <typeparam name="T">The type of resource one can subscribe to</typeparam>
public interface ISubscribable<T>
{
    public delegate void ResourceAddedCallback(T resource);

    public delegate void ResourceRemovedCallback(T resource);

    public IUnsubscribable SubscribeToResource(ResourceAddedCallback added, ResourceRemovedCallback removed);
}
