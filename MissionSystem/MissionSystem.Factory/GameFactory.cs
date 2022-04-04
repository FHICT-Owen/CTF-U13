using MissionSystem.Logic;
using MissionSystem.Logic.CTF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionSystem.Factory
{
    public class GameFactory
    {
        public static BaseGame GetBaseGame(IServiceProvider provider)
        {
            return new CTFLogic(provider);
        }
    }
}
