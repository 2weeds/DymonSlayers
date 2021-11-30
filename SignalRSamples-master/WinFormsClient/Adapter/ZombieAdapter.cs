using SgClient1.Classes_Test;
using System.Windows.Forms;

namespace SgClient1.Adapter
{
    public class ZombieAdapter : IAdapter
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
