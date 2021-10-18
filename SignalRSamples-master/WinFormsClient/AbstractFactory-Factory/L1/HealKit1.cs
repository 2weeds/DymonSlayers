using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SgClient1
{
    public class HealKit1 : HealKit
    { 
        public override void updateState(FormGame form, int x, int y)
        {
            PictureBox healthKit = new PictureBox();
            healthKit.Image = Properties.Resources.med_Image;
            healthKit.SizeMode = PictureBoxSizeMode.AutoSize;
            healthKit.Left = x;
            healthKit.Top = y;
            healthKit.Name = "healthKit";
            form.Controls.Add(healthKit);
            healthKit.BringToFront();
        }
    }
}
