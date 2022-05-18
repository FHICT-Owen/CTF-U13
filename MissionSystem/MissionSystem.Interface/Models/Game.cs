using MissionSystem.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionSystem.Interface
{
    public class Game
    {
        public string Key { get; set; }
        public string GameType { get; set; }
        public Arena Arena { get; set; }
        public IBaseGame BaseGame { get; set; }
    }
}
