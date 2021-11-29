using System.Collections;

namespace SgClient1
{
    public abstract class AbstractFactory
    {
        public Hashtable hash = new Hashtable();
        public abstract Unit createGunBullets();
        public abstract Unit createHealKit();
        public abstract Unit createFireWall();
    }
}
