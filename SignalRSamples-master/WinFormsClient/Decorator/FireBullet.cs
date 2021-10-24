using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Decorator
{
    class FireBullet : Decorator
    {
        public FireBullet(PlayerClass component) : base (component)
        {
        }
        public void FireShoot(string direct)
        {
            shoot(direct, "Fire");
        }

        private void shoot(string direct, string bulletType)
        {
            base.shoot(direct, bulletType);
        }
    } 
}
