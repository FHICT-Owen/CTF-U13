namespace MissionSystem.Main.Time;

public interface ITimer
{
    public event EventHandler<ITimer> Start;
    public event EventHandler<ITimer> Update;
    public event EventHandler<ITimer> Stop;
    public event EventHandler<ITimer> Pause;
    public event EventHandler<ITimer> Continue;

    public int Time { get; }
    public int? TimeRemaining { get; }
    public int TotalDuration { get; }
    public DateTime? StartTime { get; }
    public DateTime? EndTime { get; }
    public bool IsRunning { get; }

    public void StartTimer();
    public void StopTimer();
    public void PauseTimer();
    public void ContinueTimer();
    public void OnTick(object? sender, EventArgs e);
}