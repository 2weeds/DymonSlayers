using System;
using System.Drawing;
using System.Windows.Forms;

namespace SgClient1.Classes_Test
{
    public abstract class Entity : GameClass
    {
        public int Health { get; set; }
        public Weapon Weapon { get; set; }
        public object Personality { get; set; }

        public abstract void TakeDamage(int damage);

        public void shoot(FormGame form, string bulletType, int bulletLeft, int bulletTop, string direction)
        {
            PictureBox bullet = new PictureBox();
            Timer tm = new Timer();
            int speed = 20;
            create(form, bulletType, tm, bullet, bulletLeft, bulletTop);
            tm.Interval = speed;
            tm.Tick += delegate (object sender, EventArgs e)
            {
                travel(sender, e, direction, bullet);
            };
            tm.Start();
            contact(tm, bullet);
        }

        public void create(FormGame form, string bulletType, Timer tm, PictureBox bullet, int bulletLeft, int bulletTop)
         {
            if (bulletType == "Fire")
            {
                bullet.BackColor = System.Drawing.Color.Orange;
                bullet.Name = "bulletF";
            }
            else if (bulletType == "Ice")
            {
                bullet.BackColor = System.Drawing.Color.White;
                bullet.Name = "bulletI";
            }
            else if (bulletType == "Lightning")
            {
                bullet.BackColor = System.Drawing.Color.Yellow;
                bullet.Name = "bulletL";
            }
            bullet.Size = new Size(5, 5);
            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;
            bullet.BringToFront();
            form.Controls.Add(bullet);
        }

        public void travel(object sender, EventArgs e, string direction, PictureBox bullet)
        {
            int speed = 20;
            if (direction == "left")
            {
                bullet.Left -= speed;
            }

            if (direction == "right")
            {
                bullet.Left += speed;
            }

            if (direction == "up")
            {
                bullet.Top -= speed;
            }

            if (direction == "down")
            {
                bullet.Top += speed;
            }
        }

        public void contact(Timer tm, PictureBox bullet)
        {
            if (bullet.Left < 16 || bullet.Left > 860 || bullet.Top < 10 || bullet.Top > 616)
            {
                tm.Stop();
                tm.Dispose();
                bullet.Dispose();
            }
        }
    }
}
