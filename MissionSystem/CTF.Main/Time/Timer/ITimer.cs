namespace CTF.Main.Time;

public interface ITimer
{
    public event EventHandler<Timer> Start;
    public event EventHandler<Timer> Update;
    public event EventHandler<Timer> Stop;
    public event EventHandler<Timer> Pause;
    public event EventHandler<Timer> Continue;

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
}