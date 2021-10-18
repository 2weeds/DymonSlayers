using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1
{
    class L3Factory : AbstractFactory
    {
        public override Unit createGunBullets()
        {
            return new AmmoKit3();
        }

        public override Unit createHealKit()
        {
            return new HealKit3();
        }

        public override Unit createFireWall()
        {
            return new FireWall3();
        }
    }
}
