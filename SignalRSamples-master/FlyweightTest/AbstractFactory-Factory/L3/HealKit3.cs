using System.Windows.Forms;

namespace FlyweightTest
{
    public class HealKit3 : HealKit
    {
        public override void spawnUnit(FormGame form, int x, int y)
        {
            PictureBox healthKit = new PictureBox();
            healthKit.Image = Properties.Resources.med_Image2;
            healthKit.SizeMode = PictureBoxSizeMode.AutoSize;
            healthKit.Left = x;
            healthKit.Top = y;
            healthKit.Name = "healthKit2";
            form.Controls.Add(healthKit);
            healthKit.BringToFront();
        }
    }
}
