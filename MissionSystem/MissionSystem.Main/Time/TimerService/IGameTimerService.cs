namespace MissionSystem.Main.Time;

public interface IGameTimerService
{
    public ITimer CreateTimer(int durationInSeconds);
    public void DeleteTimer(ITimer timer);
}