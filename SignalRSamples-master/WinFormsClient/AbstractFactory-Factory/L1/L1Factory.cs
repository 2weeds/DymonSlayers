namespace SgClient1
{
    class L1Factory : AbstractFactory
    {
        public override Unit createGunBullets()
        {
            return new AmmoKit1();
        }

        public override Unit createHealKit()
        {
            return new HealKit1();
        }

        public override Unit createFireWall()
        {
            return new FireWall1();
        }
    }
}
