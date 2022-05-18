using MissionSystem.Interface.Services;
using MissionSystem.Logic;
using MissionSystem.Logic.CTF;

namespace MissionSystem.Factory
{
    public class GameFactory
    {
        public static void RegisterGameTypes(IGameTypeService gameTypeService)
        {
            gameTypeService.RegisterGameType("ctf", new CtfGameType());
        }

        public static BaseGame GetBaseGame(IServiceProvider provider)
        {
            return new CTFLogic(provider);
        }
    }
}
