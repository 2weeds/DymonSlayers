using System.Windows.Forms;

namespace SgClient1
{
    public class AmmoKit3 : AmmoKit
    {
        public override void spawnUnit(FormGame form, int x, int y)
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo_Image2;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = x;
            ammo.Top = y;
            ammo.Name = "ammo2";
            form.Controls.Add(ammo);
            ammo.BringToFront();
        }
    }
}
