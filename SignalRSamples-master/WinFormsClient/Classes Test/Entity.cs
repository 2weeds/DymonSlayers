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

        public abstract void create(FormGame form, string bulletType, Timer tm, PictureBox bullet, int bulletLeft, int bulletTop);
        public abstract void travel(object sender, EventArgs e, string direction, PictureBox bullet);

        public abstract void contact(Timer tm, PictureBox bullet);
        public abstract string ReportToServer();
    }
}
