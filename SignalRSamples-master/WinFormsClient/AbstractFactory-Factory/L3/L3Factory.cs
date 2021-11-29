namespace SgClient1
{
    class L3Factory : AbstractFactory
    {
        public override Unit createGunBullets()
        {
            if (!hash.ContainsKey("ammo3"))
            {
                hash.Add("ammo3", Properties.Resources.ammo_Image2);
            }
            return new AmmoKit3();
        }

        public override Unit createHealKit()
        {
            if (!hash.ContainsKey("heal3"))
            {
                hash.Add("heal3", Properties.Resources.med_Image2);
            }
            return new HealKit3();
        }

        public override Unit createFireWall()
        {
            if (!hash.ContainsKey("fire3"))
            {
                hash.Add("fire3", Properties.Resources.fireWall_Image2);
            }
            return new FireWall3();
        }
    }
}
