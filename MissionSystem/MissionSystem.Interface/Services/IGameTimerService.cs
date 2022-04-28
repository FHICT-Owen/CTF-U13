using MissionSystem.Interface.Timer;

namespace MissionSystem.Interface.Services;

public interface IGameTimerService
{
    public ITimer CreateTimer(int durationInSeconds);
    public void DeleteTimer(ITimer timer);
}