using MissionSystem.Interface.Models;
using MissionSystem.Interface.Services;
using MissionSystem.Logic;
using MissionSystem.Logic.CTF;

namespace MissionSystem.Factory
{
    public class GameFactory
    {
        public static void RegisterGameTypes(IGameTypeService gameTypeService)
        {
            gameTypeService.RegisterGameType("ctf", new CtfGameType("Capture The Flags", 3));
            gameTypeService.RegisterGameType("koh", new CtfGameType("King Of The Hill", 1));
        }

        public static BaseGame GetBaseGame(IServiceProvider provider, Arena arena)
        {
            return new CTFLogic(provider, arena);
        }
    }
}
