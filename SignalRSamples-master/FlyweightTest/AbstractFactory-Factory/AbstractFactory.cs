using System.Collections;

namespace FlyweightTest
{
    public abstract class AbstractFactory
    {
        public abstract Unit createGunBullets();
        public abstract Unit createHealKit();
        public abstract Unit createFireWall();
    }
}
