using MissionSystem.Interface;
using MissionSystem.Main.Gadgets;

namespace MissionSystem.Util;

public abstract class SubscribableResource<T, TId> : ISubscribable<T, TId>
{
    private readonly HashSet<ISubscribable<T, TId>.ResourceAddedCallback> _addedCallbacks = new();
    private readonly HashSet<ISubscribable<T, TId>.ResourceRemovedCallback> _removedCallbacks = new();

    public IUnsubscribable SubscribeToResource(ISubscribable<T, TId>.ResourceAddedCallback added,
        ISubscribable<T, TId>.ResourceRemovedCallback removed)
    {
        _addedCallbacks.Add(added);
        _removedCallbacks.Add(removed);

        return new ResourceUnsubscribable(
            new Unsubscribable<ISubscribable<T, TId>.ResourceAddedCallback>(_addedCallbacks, added),
            new Unsubscribable<ISubscribable<T, TId>.ResourceRemovedCallback>(_removedCallbacks, removed));
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
