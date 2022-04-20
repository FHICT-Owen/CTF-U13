using MissionSystem.Factory;
using MissionSystem.Interface;

namespace MissionSystem.Main;
public class GameService : IGameService
{
    private IServiceProvider serviceProvider;
    public GameService(IServiceProvider provider)
    {
        serviceProvider = provider;
    }
    public IBaseGame GetGame(string type)
    {
        // if (type == "ctf") return GameFactory.GetBaseGame(serviceProvider);
        return GameFactory.GetBaseGame(serviceProvider);
    }
}
