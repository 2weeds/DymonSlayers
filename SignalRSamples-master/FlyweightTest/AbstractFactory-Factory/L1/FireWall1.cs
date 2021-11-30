using System.Windows.Forms;

namespace FlyweightTest
{
    public class FireWall1 : FireWall
    {
        public override void spawnUnit(FormGame form, int x, int y)
        {
            PictureBox fireWall = new PictureBox();
            fireWall.Image = Properties.Resources.fireWall_Image;
            fireWall.SizeMode = PictureBoxSizeMode.AutoSize;
            fireWall.Left = x;
            fireWall.Top = y;
            fireWall.Name = "fireWall";
            form.Controls.Add(fireWall);
            fireWall.BringToFront();
        }
    }
}
