using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.State
{
    internal class StateDamaged : HealthState
    {
        public StateDamaged(PlayerClass player) : base(player)
        {
        }

        public override void SetSpeed()
        {
            if (player.Health <= 50)
            {//sets speed as defined in strategy at the making of this (lvl1 = 5, lvl2 = 7, lvl3 = 10) but adds 4
                player.speed = (int)(Math.Pow(player.level, 2) / 2) + (player.level / 2) + 4 +20;
            }
            else
            {
                ChangeState();
            }
        }
    }
}
