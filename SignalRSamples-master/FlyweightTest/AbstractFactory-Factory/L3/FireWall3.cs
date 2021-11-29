﻿using System.Windows.Forms;

namespace FlyweightTest
{
    public class FireWall3 : FireWall
    {
        public override void spawnUnit(FormGame form, int x, int y)
        {
            PictureBox fireWall = new PictureBox();
            fireWall.Image = Properties.Resources.fireWall_Image2;
            fireWall.SizeMode = PictureBoxSizeMode.AutoSize;
            fireWall.Left = x;
            fireWall.Top = y;
            fireWall.Name = "fireWall2";
            form.Controls.Add(fireWall);
            fireWall.BringToFront();
        }
    }
}
