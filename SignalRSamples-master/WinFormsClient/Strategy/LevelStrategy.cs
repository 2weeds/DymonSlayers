using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Strategy
{
    abstract class LevelStrategy
    {
        public abstract void action(PlayerClass player, Zombie zombie);
    }
}
