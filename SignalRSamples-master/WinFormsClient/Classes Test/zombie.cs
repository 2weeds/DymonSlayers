using SgClient1.Adapter;
using SgClient1.Iterator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SgClient1.Classes_Test
{
    public class Zombie : Entity
    {
        ZombieAdapter adapter;
        Random rnd = new Random();
        public int speed = 2;
        //PictureBox zombie = new PictureBox();
        public int zombieLeft;
        public int zombieTop;
        private ZombieNameCollection names = new ZombieNameCollection();
        private ZombiesCollection zombies = new ZombiesCollection();
        public int ZombieDamageToPlayer = 1;

        public bool ContainsName(string name)
        {
            if (names.Contains(name))
                return true;
            return false;
        }

        public Zombie Find(string name)
        {
            for(var z = zombies.CreateIterator(); z.HasMore();)
            {
                Zombie currItem = (Zombie) z.GetNext();
                if (currItem.Name == name)
                {
                    return currItem;
                }
            }
            return null;
        }
        public void RemoveZombie(Zombie z)
        {
            zombies.RemoveItem(z);
        }
        public override void TakeDamage(int damage)
        {
            Health -= damage;
        }
        public string UnusedName()
        {
            string UnusedName = "";
            for (var nm = names.CreateIterator(); nm.HasMore();)
            {
                string currItem = (string)nm.GetNext();
                bool used = false;
                for (var z = zombies.CreateIterator(); z.HasMore();)
                {
                    Zombie zomb = (Zombie)z.GetNext();
                    if (currItem == zomb.Name)
                    {
                        used = true;
                        break;
                    }
                }
                if (!used)
                {
                    UnusedName = currItem;
                    break;
                }
            }
            return UnusedName;
        }
        public void createAZombie(FormGame form, int x, int y, Weapon weapon)
        {
            Zombie zombie = new Zombie();
            zombie.form = form;
            zombie.player = player;
            zombie.player1 = player1;
            zombie.Weapon = weapon;
            zombie.Health = 3;
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombie.Name = UnusedName();
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = x;
            zombie.Top = y;
            zombie.BringToFront();
            form.Controls.Add(zombie);
            zombies.AddItem(zombie);
        }
        public override string ReportToServer()
        {
            return String.Format("Zombie {0} was created", this.Name);
        }
        public void Scratch(PlayerClass playerClass, Control x)
        {
            Mediator.Mediator mediator = new Mediator.Mediator(playerClass, this);
            mediator.Interaction(playerClass);
        }
        public void zombieInteractions(PlayerClass playerClass, int chaseCase)
        {
            foreach (Control x in form.Controls)
            {
                if (x is PictureBox && names.Contains(x.Name))
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(playerClass.player.Bounds))
                    {
                        //playerClass.TakeDamage(Find(x.Name).Weapon.GetWeaponDamage());      //zombie do dmg to player
                        //Scratch(playerClass, x);
                        new ZombieAdapter(this).DoDamage(playerClass, x);
                        if (playerClass.GetHealth() == 30 || playerClass.GetHealth() == 20)
                        {
                            _hubProxy.Invoke("UpdateHealthPacks", group, rnd.Next(10, 790), rnd.Next(50, 500));
                        }
                    }
                    var p = playerClass.player;
                    if (chaseCase == 1)
                    {
                        p = playerClass.player1;
                    }
                    else if (chaseCase == 2)
                    {
                        int[] distances = new int[4];                                          // Array with zombie distances to player: Indexes 0 and 1 are for player 1, indexes 2 and 3 are for player 2
                        distances[0] = System.Math.Abs(((PictureBox)x).Left - playerClass.player.Left);
                        distances[1] = System.Math.Abs(((PictureBox)x).Top - playerClass.player.Top);
                        distances[2] = System.Math.Abs(((PictureBox)x).Left - playerClass.player1.Left);
                        distances[3] = System.Math.Abs(((PictureBox)x).Top - playerClass.player1.Top);
                        int min = 999999;
                        int ind = -1;
                        for (int i = 0; i < distances.Length; i++)
                        {
                            if (distances[i] < min)
                            {
                                min = distances[i];
                                ind = i;
                            }
                        }
                        if (ind == 2 || ind == 3)
                        {
                            p = playerClass.player1;
                        }
                    }

                    if (names.Contains(((PictureBox)x).Name))
                    {

                        if (((PictureBox)x).Left > p.Left)
                        {
                            ((PictureBox)x).Left -= speed;
                            ((PictureBox)x).Image = Properties.Resources.zleft;
                        }

                        if (((PictureBox)x).Left < p.Left)
                        {
                            ((PictureBox)x).Left += speed;
                            ((PictureBox)x).Image = Properties.Resources.zright;
                        }

                        if (((PictureBox)x).Top > p.Top)
                        {
                            ((PictureBox)x).Top -= speed;
                            ((PictureBox)x).Image = Properties.Resources.zup;
                        }

                        if (((PictureBox)x).Top < p.Top)
                        {
                            ((PictureBox)x).Top += speed;
                            ((PictureBox)x).Image = Properties.Resources.zdown;
                        }
                    }
                }
            }
        }

        public override void create(FormGame form, string bulletType, Timer tm, PictureBox bullet, int bulletLeft, int bulletTop)
        {
            throw new NotImplementedException();
        }

        public override void travel(object sender, EventArgs e, string direction, PictureBox bullet)
        {
            throw new NotImplementedException();
        }

        public override void contact(Timer tm, PictureBox bullet)
        {
            throw new NotImplementedException();
        }
    }
}
