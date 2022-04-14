using Microsoft.Extensions.DependencyInjection;
using MissionSystem.Interface;
using MissionSystem.Interface.Timer;

namespace MissionSystem.Logic;
public abstract class BaseGame
{
    private IGameTimerService gameTimerService;
    protected IGameTimer timer { get; set; }

    
    public BaseGame(IServiceProvider provider)
    {
        gameTimerService = provider.GetService<IGameTimerService>();
    }

    public abstract Task Setup();

    public abstract Task Start();

    protected void CreateTimer(int duration)
    {
        gameTimerService.CreateTimer(duration);
    }
}

