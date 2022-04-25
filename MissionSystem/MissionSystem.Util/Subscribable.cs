using MissionSystem.Interface;
using MissionSystem.Main.Gadgets;

namespace MissionSystem.Util;

public abstract class SubscribableResource<T> : ISubscribable<T>
{
    protected readonly HashSet<ISubscribable<T>.ResourceAddedCallback> AddedCallbacks = new();
    protected readonly HashSet<ISubscribable<T>.ResourceRemovedCallback> RemovedCallbacks = new();

    public IUnsubscribable SubscribeToResource(ISubscribable<T>.ResourceAddedCallback added,
        ISubscribable<T>.ResourceRemovedCallback removed)
    {
        AddedCallbacks.Add(added);
        RemovedCallbacks.Add(removed);

        return new ResourceUnsubscribable(
            new Unsubscribable<ISubscribable<T>.ResourceAddedCallback>(AddedCallbacks, added),
            new Unsubscribable<ISubscribable<T>.ResourceRemovedCallback>(RemovedCallbacks, removed));
    }

    private readonly struct ResourceUnsubscribable : IUnsubscribable
    {
        private readonly IDisposable _added;
        private readonly IDisposable _removed;

        public ResourceUnsubscribable(IDisposable added, IDisposable removed)
        {
            _added = added;
            _removed = removed;
        }

        public void Dispose()
        {
            _added.Dispose();
            _removed.Dispose();
        }
    }
}
