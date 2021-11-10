using SgClient1.Classes_Test;

namespace SgClient1.Decorator
{
    public class LightningBullet : Decorator
    {
        public LightningBullet(PlayerClass component) : base(component)
        {
        }
        public void LightningShoot(string direct)
        {
            shoot(direct, "Lightning");
        }

        private void shoot(string direct, string bulletType)
        {
            base.shoot(direct, bulletType);
        }
    }
}
