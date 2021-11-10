using SgClient1.Classes_Test;

namespace SgClient1.Decorator
{
    public class FireBullet : Decorator
    {
        public FireBullet(PlayerClass component) : base(component)
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
