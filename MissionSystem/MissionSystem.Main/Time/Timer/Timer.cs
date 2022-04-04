namespace MissionSystem.Main.Time;

public class Timer : EventArgs, ITimer
{
    public event EventHandler<ITimer>? Update;

    public event EventHandler<ITimer>? Start;
    public event EventHandler<ITimer>? Stop;
    public event EventHandler<ITimer>? Pause;
    public event EventHandler<ITimer>? Continue;


    private int _Time = 0;
    public int Time { get { return _Time; } }

    private bool _IsRunning = false;
    public bool IsRunning { get { return _IsRunning; } }

    public int? TimeRemaining { get { return TotalDuration - Time; } }

    private int _TotalDuration;
    public int TotalDuration { get { return _TotalDuration; } }

    private DateTime? _StartTime;
    public DateTime? StartTime { get { return _StartTime;} }

    private DateTime? _EndTime;
    public DateTime? EndTime { get { return _EndTime;} }

    public Timer(int totalDuration)
    {
        _TotalDuration = totalDuration;
    }

    public void OnTick(object? sender, EventArgs e)
    {
        if (TimeRemaining <= 0)
        {
            StopTimer();
            return;
        }

        this._Time++;
        Update?.Invoke(sender, this);
    }

    public void StartTimer()
    {
        if (IsRunning) return;

        if (StartTime == null)
        {
            _StartTime = DateTime.Now;
        }
        
        _IsRunning = true;
        Start?.Invoke(this, this);
    }

    public void StopTimer()
    {
        if (!IsRunning) return;

        if (StartTime != null)
        {
            _EndTime = DateTime.Now;
        }

        _IsRunning = false;
        Stop?.Invoke(this, this);
    }

    public void PauseTimer()
    {
        if (!IsRunning) return;

        _IsRunning = false;
        Pause?.Invoke(this, this);
    }

    public void ContinueTimer()
    {
        if (IsRunning) return;

        _IsRunning = true;
        Continue?.Invoke(this, this);
    }
}