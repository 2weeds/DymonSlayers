﻿using SgClient1.Observer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SgClient1.Classes_Test
{
    public class PlayerClass : Entity, ISubject
    {
        //public int Health = 100;
        public int speed = 0;
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
        private List<IObserver> _observers;
        public PlayerClass()
        {
            _observers = new List<IObserver>();
        }

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

        public void DoDamage(Zombie zombie, Control x)
        {
            Mediator.Mediator mediator = new Mediator.Mediator(this, zombie);
            mediator.Interaction(zombie);
            //zombie.TakeDamage(this.Weapon.GetWeaponDamage());
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

            Mediator.Mediator mediator = new Mediator.Mediator();
            foreach (Control x in form.Controls)
            {
                if (x is PictureBox && x.Name == "ammo" || x is PictureBox && x.Name == "ammo1" || x is PictureBox && x.Name == "ammo2")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds) || ((PictureBox)x).Bounds.IntersectsWith(player1.Bounds))
                    {
                        form.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                        if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds)) { ammo += mediator.Interaction(x.Name); }
                    }
                }

                if (x is PictureBox && x.Name == "healthKit" || x is PictureBox && x.Name == "healthKit1" || x is PictureBox && x.Name == "healthKit2")
                {

                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds) || ((PictureBox)x).Bounds.IntersectsWith(player1.Bounds))
                    {
                        form.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                        if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds)) { Health += mediator.Interaction(x.Name); if (Health > 100) { Health = 100; } }
                    }
                }

                if (x is PictureBox && x.Name == "fireWall" || x is PictureBox && x.Name == "fireWall1" || x is PictureBox && x.Name == "fireWall2")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds) || ((PictureBox)x).Bounds.IntersectsWith(player1.Bounds))
                    {
                        form.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                        if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                        {
                            if (Health < mediator.Interaction(x.Name))
                                Health = 0;
                            else
                                Health -= mediator.Interaction(x.Name);
                        }
                    }
                }
            }
            return false;
        }

        public void shoot(string direct, string bulletType)
        {
            shoot(form, bulletType, player.Left + (player.Width / 2), player.Top + (player.Height / 2), direct);
            if (_hubProxy != null)
                _hubProxy.Invoke("Send", $"s;{direct};{bulletType};{player.Left + (player.Width / 2)};{player.Top + (player.Height / 2)}");
        }

        public override void create(FormGame form, string bulletType, Timer tm, PictureBox bullet, int bulletLeft, int bulletTop)
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

        public override void travel(object sender, EventArgs e, string direction, PictureBox bullet)
        {
            int bSpeed = 20;
            if (direction == "left")
            {
                bullet.Left -= bSpeed;
            }

            if (direction == "right")
            {
                bullet.Left += bSpeed;
            }

            if (direction == "up")
            {
                bullet.Top -= bSpeed;
            }

            if (direction == "down")
            {
                bullet.Top += bSpeed;
            }
        }

        public override void contact(Timer tm, PictureBox bullet)
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
