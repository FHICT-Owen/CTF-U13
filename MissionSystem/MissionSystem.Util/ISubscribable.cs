using MissionSystem.Interface;

namespace MissionSystem.Util;

public interface ISubscribable<T, TId>
{
    public delegate void ResourceAddedCallback(T resource);

    public delegate void ResourceRemovedCallback(TId id);

    public IUnsubscribable SubscribeToResource(ResourceAddedCallback added, ResourceRemovedCallback removed);
}
