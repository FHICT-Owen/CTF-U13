namespace MissionSystem.Interface.Timer;
public interface IBaseTimer : IDisposable
{
    public event EventHandler Start;
    public event EventHandler Update;
    public event EventHandler Stop;
    public event EventHandler Reset;

    public int Time { get; }
    public int? TimeRemaining { get; }
    public int TotalDuration { get; }
    public bool IsRunning { get; }
    public DateTime? StartTime { get; }
    public DateTime? EndTime { get; }

}
