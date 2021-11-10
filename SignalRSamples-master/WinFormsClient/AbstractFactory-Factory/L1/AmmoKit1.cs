using System.Windows.Forms;

namespace SgClient1
{
    public class AmmoKit1 : AmmoKit
    {
        public override void spawnUnit(FormGame form, int x, int y)
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo_Image;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = x;
            ammo.Top = y;
            ammo.Name = "ammo";
            form.Controls.Add(ammo);
            ammo.BringToFront();
        }
    }
}
