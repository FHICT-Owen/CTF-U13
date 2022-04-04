using MissionSystem.Interface.Timer;

namespace MissionSystem.Interface;

public interface IGameTimerService
{
    public ITimer CreateTimer(int durationInSeconds);
    public void DeleteTimer(ITimer timer);
}