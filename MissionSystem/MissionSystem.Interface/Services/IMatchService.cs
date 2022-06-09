using MissionSystem.Interface.Models;
using MissionSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionSystem.Interface.Services;
public interface IMatchService : ISubscribableResource<Match>
{
    public Task CreateMatchAsync(Match match);
    public Task DeleteMatchAsync(Match match);
    public Task UpdateMatchAsync(Match match);
    public Task<Match> FindMatchAsync(string id);
    public Task<List<Match>> GetMatchesAsync();
}