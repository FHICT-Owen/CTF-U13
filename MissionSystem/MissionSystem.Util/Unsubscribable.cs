namespace MissionSystem.Util;

public class Unsubscribable<T> : IUnsubscribable
{
    private readonly T _callback;

    private readonly HashSet<T> _callbacks;

    public Unsubscribable(HashSet<T> callbacks, T callback)
    {
        _callbacks = callbacks;
        _callback = callback;
    }

    public void Dispose()
    {
        _callbacks.Remove(_callback);
    }
}
