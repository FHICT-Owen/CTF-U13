using MissionSystem.Interface.Timer;

namespace MissionSystem.Interface;
public interface IBaseGame : IFrontendData
{
    public abstract Task Setup();
    public ITimer GetTimer();
    public ITimer GetNewTimer();
    public abstract Task Start();
}
