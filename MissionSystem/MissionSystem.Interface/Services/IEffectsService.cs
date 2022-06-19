using Microsoft.Extensions.Hosting;

namespace MissionSystem.Interface.Services;

public interface IEffectsService: IHostedService
{
    public Task TriggerEffectAsync(string effect);
}
