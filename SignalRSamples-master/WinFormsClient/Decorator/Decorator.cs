using SgClient1.Classes_Test;

namespace SgClient1.Decorator
{
    public abstract class Decorator
    {
        protected PlayerClass wrappee;
        public Decorator(PlayerClass component)
        {
            wrappee = component;
        }

        public void shoot(string direct, string bulletType)
        {
            wrappee.shoot(direct, bulletType);
        }
    }
}
