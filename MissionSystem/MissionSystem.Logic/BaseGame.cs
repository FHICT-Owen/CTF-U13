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

    public abstract event EventHandler<string>? data;

    protected ITimer timer { get; set; }

    
    public BaseGame(IServiceProvider provider)
    {
        gameTimerService = provider.GetService<IGameTimerService>();
        gadgetStateService = provider.GetService<IGadgetStateService>();
        gadgetSettingsService = provider.GetService<IGadgetSettingsService>();

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

