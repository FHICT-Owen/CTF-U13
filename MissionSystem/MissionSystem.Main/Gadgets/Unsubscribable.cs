using MissionSystem.Interface;

namespace MissionSystem.Main.Gadgets;

public class Unsubscribable : IUnsubscribable
{
    private readonly IGadgetStateService.StateCallback _callback;

    private readonly HashSet<IGadgetStateService.StateCallback> _callbacks;

    public Unsubscribable(HashSet<IGadgetStateService.StateCallback> callbacks, IGadgetStateService.StateCallback callback)
    {
        _callbacks = callbacks;
        _callback = callback;
    }

    public void Dispose()
    {
        _callbacks.Remove(_callback);
    }
}
