using SgClient1.Classes_Test;

namespace SgClient1.Decorator
{
    public class IceBullet : Decorator
    {
        public IceBullet(PlayerClass component) : base(component)
        {
        }
        public void IceShoot(string direct)
        {
            shoot(direct, "Ice");
        }

        private void shoot(string direct, string bulletType)
        {
            base.shoot(direct, bulletType);
        }
    }
}
