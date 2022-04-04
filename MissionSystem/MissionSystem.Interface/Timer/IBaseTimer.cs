namespace MissionSystem.Interface.Timer;
public interface IBaseTimer
{
    public event EventHandler Start;
    public event EventHandler Update;
    public event EventHandler Stop;
    public event EventHandler Pause;
    public event EventHandler Continue;

    public int Time { get; }
    public int? TimeRemaining { get; }
    public int TotalDuration { get; }
    public bool IsRunning { get; }
    public DateTime? StartTime { get; }
    public DateTime? EndTime { get; }

}
