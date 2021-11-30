using System.Windows.Forms;

namespace FlyweightTest
{
    public class AmmoKit2 : AmmoKit
    {
        public override void spawnUnit(FormGame form, int x, int y)
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = p.getPickup(Properties.Resources.ammo_Image1, "ammo1");
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Name = "ammo1";
            ammo.Left = x;
            ammo.Top = y;
            form.Controls.Add(ammo);
            ammo.BringToFront();
        }
    }
}
