using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SgClient1
{
    public class FireWall2 : FireWall
    { 
        public override void spawnUnit(FormGame form, int x, int y)
        {
            PictureBox fireWall = new PictureBox();
            fireWall.Image = Properties.Resources.fireWall_Image1;
            fireWall.SizeMode = PictureBoxSizeMode.AutoSize;
            fireWall.Left = x;
            fireWall.Top = y;
            fireWall.Name = "fireWall1";
            form.Controls.Add(fireWall);
            fireWall.BringToFront();
        }
    }
}
