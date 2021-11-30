namespace SgClient1
{
    class L1Factory : AbstractFactory
    {

        public override Unit createGunBullets()
        {
            if (!hash.ContainsKey("ammo1"))
            {
                hash.Add("ammo1", Properties.Resources.ammo_Image);
            }
            return new AmmoKit1();
        }

        public override Unit createHealKit()
        {
            if (!hash.ContainsKey("heal1"))
            {
                hash.Add("heal1", Properties.Resources.med_Image);
            }
            return new HealKit1();
        }

        public override Unit createFireWall()
        {
            if (!hash.ContainsKey("fire1"))
            {
                hash.Add("fire1", Properties.Resources.fireWall_Image);
            }
            return new FireWall1();
        }
    }
}
