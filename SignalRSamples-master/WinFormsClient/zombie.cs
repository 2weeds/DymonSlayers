using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SgClient1
{
    class zombie
    {
        public int speed = 3;
        PictureBox Zombie = new PictureBox();
        public int zombieLeft;
        public int zombieTop;

        public void createAZombie(FormGame form)
        {
            Zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            Zombie.Name = "zombie";
            Zombie.Image = Properties.Resources.zdown;
            Zombie.Left = zombieLeft;
            Zombie.Top = zombieTop;
            Zombie.BringToFront();
            form.Controls.Add(Zombie);
        }
    }
}
