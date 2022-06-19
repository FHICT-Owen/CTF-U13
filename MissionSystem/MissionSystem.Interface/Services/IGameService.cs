using MissionSystem.Interface.Models;

namespace MissionSystem.Interface.Services;

public interface IGameService : ISubscribableResource<Match>
{
    public IBaseGame GetBaseGame(Match match);
    public Task DeleteMatchAsync(Match match);
    public Task CreateMatchAsync(Match match);
    public void DeleteMatchesByArenaAsync(Arena arena);
    public Task<Match> FindMatchById(int id);
}
