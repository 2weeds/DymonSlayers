using System.Windows.Forms;

namespace SgClient1
{
    public class AmmoKit1 : AmmoKit
    {
        public override void spawnUnit(FormGame form, int x, int y)
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = p.getPickup(Properties.Resources.ammo_Image, "ammo");
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Name = "ammo";
            ammo.Left = x;
            ammo.Top = y;
            form.Controls.Add(ammo);
            ammo.BringToFront();
        }
    }
}
