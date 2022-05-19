using MissionSystem.Interface.Timer;

namespace MissionSystem.Interface;
public interface IBaseGame : IFrontendData, IDisposable
{
    public abstract Task Setup();
    public abstract string GetData();
    public ITimer GetTimer();
    public abstract Task Start();
    public abstract T? Get<T>(string variable);
}
