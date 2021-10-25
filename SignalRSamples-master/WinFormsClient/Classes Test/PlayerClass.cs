using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SgClient1.Observer;

namespace SgClient1.Classes_Test
{
    class PlayerClass : Entity, ISubject
    {
        //public int Health = 100;
        public int speed = 10;
        public int ammo = 10;
        public int Health
        {
            get { return _health; }
            set
            {
                _health = value;
                Notify();
            }
        }
        private int _health;
        public PlayerClass()
        {
            _observers = new List<IObserver>();
        }
        
        
        private List<IObserver> _observers;


        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Notify()
        {
            _observers.ForEach(o =>
            {
                o.Update(this);
            });
        }

       

        public override void TakeDamage(int damage)
        {
            Health -= damage;
        }
        public int GetHealth()
        {
            return Health;
        }
        public bool playerGameEngine(ProgressBar progressBar, bool goleft, bool goup, bool goright, bool godown)
        {
            if (Health > 1)
            {
                progressBar.Value = Convert.ToInt32(Health);
            }
            else
            {
                player.Image = Properties.Resources.dead;
                return true;
            }

            if (Health < 20)
            {
                progressBar.ForeColor = System.Drawing.Color.Red;
            }

            if (goleft && player.Left > 0)
            {
                player.Left -= speed;
                _hubProxy.Invoke("Send", $"m;left;{player.Location.X};{player.Location.Y}");
            }

            if (goright && player.Left + player.Width < 930)
            {
                player.Left += speed;
                _hubProxy.Invoke("Send", $"m;right;{player.Location.X};{player.Location.Y}");
            }

            if (goup && player.Top > 60)
            {
                player.Top -= speed;
                _hubProxy.Invoke("Send", $"m;up;{player.Location.X};{player.Location.Y}");
            }

            if (godown && player.Top + player.Height < 700)
            {
                player.Top += speed;
                _hubProxy.Invoke("Send", $"m;down;{player.Location.X};{player.Location.Y}");
            }

            foreach (Control x in form.Controls)
            {
                if (x is PictureBox && x.Name == "ammo" || x is PictureBox && x.Name == "ammo1" || x is PictureBox && x.Name == "ammo2")
                {
                    int ammoAdd = 0;
                    if (x is PictureBox && x.Name == "ammo")
                        ammoAdd = 5;
                    else if (x is PictureBox && x.Name == "ammo1")
                        ammoAdd = 10;
                    else if (x is PictureBox && x.Name == "ammo2")
                        ammoAdd = 15;
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds) || ((PictureBox)x).Bounds.IntersectsWith(player1.Bounds))
                    {
                        form.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                        if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds)) { ammo += ammoAdd; }
                    }
                }

                if (x is PictureBox && x.Name == "healthKit" || x is PictureBox && x.Name == "healthKit1" || x is PictureBox && x.Name == "healthKit2")
                {
                    int healAmount = 0;
                    if (x is PictureBox && x.Name == "healthKit")
                        healAmount = 5;
                    else if (x is PictureBox && x.Name == "healthKit1")
                        healAmount = 10;
                    else if (x is PictureBox && x.Name == "healthKit2")
                        healAmount = 15;
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds) || ((PictureBox)x).Bounds.IntersectsWith(player1.Bounds))
                    {
                        form.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                        if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds)) { Health += healAmount; if (Health > 100) { Health = 100; } }
                    }
                }

                if (x is PictureBox && x.Name == "fireWall" || x is PictureBox && x.Name == "fireWall1" || x is PictureBox && x.Name == "fireWall2")
                {
                    int damage = 0;
                    if (x is PictureBox && x.Name == "fireWall")
                        damage = 15;
                    else if (x is PictureBox && x.Name == "fireWall1")
                        damage = 25;
                    else if (x is PictureBox && x.Name == "fireWall2")
                        damage = 35;
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds) || ((PictureBox)x).Bounds.IntersectsWith(player1.Bounds))
                    {
                        form.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                        if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                        {
                            if (Health < damage)
                                Health = 0;
                            else
                                Health -= damage;
                        }
                    }
                }
            }
            return false;
        }

        public void shoot(string direct, string bulletType)
        {
            Bullet shoot = new Bullet();
            shoot.direction = direct;
            shoot.bulletLeft = player.Left + (player.Width / 2);
            shoot.bulletTop = player.Top + (player.Height / 2);
            shoot.mkBullet(form, bulletType);
            _hubProxy.Invoke("Send", $"s;{direct};{bulletType};{player.Left + (player.Width / 2)};{player.Top + (player.Height / 2)}");

        }
    }
}
