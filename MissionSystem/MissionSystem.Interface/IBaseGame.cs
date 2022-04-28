using MissionSystem.Interface.Timer;

namespace MissionSystem.Interface;
public interface IBaseGame : IFrontendData
{
    public abstract Task Setup();
    public ITimer GetTimer();
    public abstract Task Start();
}
