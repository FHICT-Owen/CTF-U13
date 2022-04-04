namespace CTF.Main.Time;

public class Timer : ITimer
{
    public event EventHandler<Timer>? Update;

    public event EventHandler<Timer>? Start;
    public event EventHandler<Timer>? Stop;
    public event EventHandler<Timer>? Pause;
    public event EventHandler<Timer>? Continue;


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

    public void OnTick(GameTimerService service)
    {
        if (TimeRemaining <= 0)
        {
            StopTimer();
            return;
        }

        this._Time++;
        Update?.Invoke(service, this);
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