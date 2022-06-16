using Microsoft.Extensions.DependencyInjection;
using MissionSystem.Interface;
using MissionSystem.Interface.Models;
using MissionSystem.Interface.Services;
using MissionSystem.Interface.Timer;

namespace MissionSystem.Logic;
public abstract class BaseGame : IBaseGame
{
    private IGameTimerService gameTimerService;
    protected IGadgetStateService gadgetStateService;
    protected IGadgetSettingsService gadgetSettingsService;
    protected IGadgetService gadgetService;
    protected IEffectsService effectsService;

    public abstract event EventHandler<string>? updateHandler;
    public abstract event EventHandler<string>? init;

    protected readonly Arena Arena;
    protected readonly Match Match;

    protected List<Gadget> Gadgets = new List<Gadget>();

    protected ITimer timer { get; set; }

    public virtual string GetData()
    {
        throw new NotImplementedException();
    }
    
    public BaseGame(IServiceProvider provider, Arena arena)
    {
        gameTimerService = provider.GetService<IGameTimerService>();
        gadgetStateService = provider.GetService<IGadgetStateService>();
        gadgetSettingsService = provider.GetService<IGadgetSettingsService>();
        gadgetService = provider.GetService<IGadgetService>();
        effectsService = provider.GetService<IEffectsService>();

        Arena = arena;
        Match = arena.Game;
    }

    public abstract Task Setup();

    public abstract Task Start();

    public abstract void ResetGame();

    public void CreateTimer(int duration)
    {
        timer = gameTimerService.CreateTimer(duration);
    }

    public ITimer GetTimer()
    {
        return timer;
    }

    async protected Task GetGadgets()
    {
        Gadgets = Match?.Gadgets.ToList();
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
}

