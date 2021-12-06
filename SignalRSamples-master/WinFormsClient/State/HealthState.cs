using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.State
{
    public abstract class HealthState
    {
        protected PlayerClass player;
        public HealthState(PlayerClass player)
        {
            this.player = player;
        }
        protected void ChangeState()
        {
            if (player.Health < 1)
            {
                player.ChangeState(new StateDead(player));
                return;
            }
            if (player.Health > 50)
            {
                player.ChangeState(new StateHealthy(player));
                return;
            }
            if (player.Health <= 50)
            {
                player.ChangeState(new StateDamaged(player));
                return;
            }
        }
        abstract public void SetSpeed();
    }
}
