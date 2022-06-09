using MissionSystem.Interface.Models;
using MissionSystem.Util;

namespace MissionSystem.Interface.Services;
public interface IGameService : ISubscribableResource<Match>
{
    public IBaseGame GetGame(string type, Arena arena);
}
