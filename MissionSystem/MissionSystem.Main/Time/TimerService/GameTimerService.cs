using MissionSystem.Interface;
using MissionSystem.Interface.Services;
using MissionSystem.Interface.Timer;

namespace MissionSystem.Main.Time;

public class GameTimerService : IGameTimerService
{
    private List<Timer> _timers = new();

    public GameTimerService(ITicker ticker)
    {
        ticker.Tick += OnTick;
    }

    public ITimer CreateTimer(int durationInSeconds)
    {
        var timer = new Timer(durationInSeconds);

        _timers.Add(timer);

        return timer;
    }

    public void DeleteTimer(ITimer timer)
    {
        _timers.Remove(timer as Timer);
    }

    private void OnTick(object? sender, EventArgs e)
    {
        foreach (var timer in _timers)
        {
            if (timer.IsRunning)
            {
                timer.OnTick(this, e);
            }
        }
    }
}