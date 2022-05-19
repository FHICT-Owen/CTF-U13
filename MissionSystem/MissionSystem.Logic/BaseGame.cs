using Microsoft.Extensions.DependencyInjection;
using MissionSystem.Interface;
using MissionSystem.Interface.Services;
using MissionSystem.Interface.Timer;

namespace MissionSystem.Logic;
public abstract class BaseGame : IBaseGame
{
    private IGameTimerService gameTimerService;
    protected IGadgetStateService gadgetStateService;
    protected IGadgetSettingsService gadgetSettingsService;
    protected IGadgetService gadgetService;

    public abstract event EventHandler<string>? updateHandler;
    public abstract event EventHandler<string>? init;


    protected ITimer timer { get; set; }

    public virtual string GetData()
    {
        throw new NotImplementedException();
    }
    
    public BaseGame(IServiceProvider provider)
    {
        gameTimerService = provider.GetService<IGameTimerService>();
        gadgetStateService = provider.GetService<IGadgetStateService>();
        gadgetSettingsService = provider.GetService<IGadgetSettingsService>();
        gadgetService = provider.GetService<IGadgetService>();

    }

    public abstract Task Setup();

    public abstract Task Start();

    protected void CreateTimer(int duration)
    {
        timer = gameTimerService.CreateTimer(duration);
    }

    public ITimer GetTimer()
    {
        return timer;
    }

    public ITimer ResetTimer()
    {
        int totalduration = timer.TotalDuration;

        timer.Dispose();

        timer = gameTimerService.CreateTimer(totalduration);

        return timer;
    }

    public void Dispose()
    {
        timer.Dispose();
    }

    public abstract T? Get<T>(string variable);
}

