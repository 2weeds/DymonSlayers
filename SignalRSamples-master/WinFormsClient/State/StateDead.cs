using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.State
{
    internal class StateDead : HealthState
    {
        public StateDead(PlayerClass player) : base(player)
        {
        }

        public override void SetSpeed()
        {
            if (player.Health < 1)
            {
                player.speed = 0;
            }
            else
            {
                ChangeState();
                player.SetSpeed();
            }
        }
    }
}
