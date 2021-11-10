using System.Windows.Forms;

namespace SgClient1
{
    public class HealKit2 : HealKit
    {
        public override void spawnUnit(FormGame form, int x, int y)
        {
            PictureBox healthKit = new PictureBox();
            healthKit.Image = Properties.Resources.med_Image1;
            healthKit.SizeMode = PictureBoxSizeMode.AutoSize;
            healthKit.Left = x;
            healthKit.Top = y;
            healthKit.Name = "healthKit1";
            form.Controls.Add(healthKit);
            healthKit.BringToFront();
        }
    }
}
