using MissionSystem.Factory;
using MissionSystem.Interface.Services;

namespace MissionSystem.Main;

public class StartupService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public StartupService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var gameTypeService = _serviceProvider.GetService<IGameTypeService>()!;

        GameFactory.RegisterGameTypes(gameTypeService);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
