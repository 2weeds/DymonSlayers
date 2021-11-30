namespace SgClient1
{
    class L2Factory : AbstractFactory
    {
        public override Unit createGunBullets()
        {
            if (!hash.ContainsKey("ammo2"))
            {
                hash.Add("ammo2", Properties.Resources.ammo_Image1);
            }
            return new AmmoKit2();
        }

        public override Unit createHealKit()
        {
            if (!hash.ContainsKey("heal2"))
            {
                hash.Add("heal2", Properties.Resources.med_Image1);
            }
            return new HealKit2();
        }

        public override Unit createFireWall()
        {
            if (!hash.ContainsKey("fire2"))
            {
                hash.Add("fire2", Properties.Resources.fireWall_Image1);
            }
            return new FireWall2();
        }
    }
}
