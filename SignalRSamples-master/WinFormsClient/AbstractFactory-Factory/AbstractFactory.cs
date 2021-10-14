using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1
{
    public abstract class AbstractFactory
    {
        public abstract Unit createGunBullets();
        public abstract Unit createHealKit();
        public abstract Unit createFireWall();
    }
}
