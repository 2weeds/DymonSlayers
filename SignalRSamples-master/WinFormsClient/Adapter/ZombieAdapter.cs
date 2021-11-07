using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SgClient1.Adapter
{
    class ZombieAdapter : IAdapter
    {
        Zombie zombie;
        public ZombieAdapter(Zombie z)
        {
            zombie = z;
        }

        public void DoDamage(Entity entity, Control x)
        {
            zombie.Scratch((PlayerClass)entity, x);
        }
    }
}
