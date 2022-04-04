namespace MissionSystem.Main.Time;

public class GameTimerService : IGameTimerService
{
    private List<Timer> _timers = new();

    public ITimer CreateTimer(int durationInSeconds)
    {
        var timer = new Timer(durationInSeconds);

        _timers.Add(timer);

        return timer;
    }

    public void DeleteTimer(Timer timer)
    {
        _timers.Remove(timer);
    }

    public void OnTick()
    {
        foreach (var timer in _timers)
        {
            if (timer.IsRunning)
            {
                timer.OnTick(this);
            }
        }
    }
}