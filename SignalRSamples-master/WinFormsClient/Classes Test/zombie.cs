using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SgClient1.Classes_Test
{
    class Zombie : Entity
    {

        Random rnd = new Random();
        public int speed = 1;
        //PictureBox zombie = new PictureBox();
        public int zombieLeft;
        public int zombieTop;
        public string[] names = { "zombie1", "zombie2", "zombie3", "zombie4" };
        public List<Zombie> zombies = new List<Zombie>();
        public Zombie Find(string name)
        {
            foreach (var z in zombies)
            {
                if (z.Name == name)
                {
                    return z;
                }
            }
            return null;
        }
        public void RemoveZombie(Zombie z)
        {
            zombies.Remove(z);
        }
        public override void TakeDamage(int damage)
        {
            Health -= damage;
        }
        public string UnusedName()
        {
            int index = -1;
            bool used = false;
            for (int i = 0; i < names.Length; i++)
            {
                used = false;
                foreach (var z in zombies)
                {
                    if (names[i] == z.Name)
                    {
                        used = true;
                        break;
                    }
                }
                if (!used)
                {
                    index = i;
                    break;
                }
            }
            return names[index];
        }
        public void createAZombie(FormGame form, int x, int y)
        {
            Zombie zombie = new Zombie();
            zombie.Health = 3;
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombie.Name = UnusedName();
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = x;
            zombie.Top = y;
            zombie.BringToFront();
            form.Controls.Add(zombie);
            zombies.Add(zombie);
        }
        public void AddZombiePB(PictureBox zpb)
        {
            Zombie zombie = new Zombie();
            zombie.Health = 3;
            zombie.Name = UnusedName();
            zombie.Left = zpb.Left;
            zombie.Top = zpb.Top;
            zombie.BringToFront();
            form.Controls.Add(zombie);
            zombies.Add(zombie);
        }

        public void zombieInteractions(PlayerClass playerClass, int chaseCase)
        {
            foreach (Control x in form.Controls)
            {
                if (x is PictureBox && names.Contains(x.Name))
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))   //zombie do dmg to player
                    {
                        playerClass.TakeDamage(1);
                        if (playerClass.GetHealth() == 30 || playerClass.GetHealth() == 20)
                        {
                            _hubProxy.Invoke("UpdateHealthPacks", group, rnd.Next(10, 790), rnd.Next(50, 500));
                        }
                    }
                    var p = player;
                    if (chaseCase == 1)
                    {
                        p = player1;
                    }
                    else if (chaseCase == 2)
                    {
                        int[] distances = new int[4];                                          // Array with zombie distances to player: Indexes 0 and 1 are for player 1, indexes 2 and 3 are for player 2
                        distances[0] = System.Math.Abs(((PictureBox)x).Left - player.Left);
                        distances[1] = System.Math.Abs(((PictureBox)x).Top - player.Top);
                        distances[2] = System.Math.Abs(((PictureBox)x).Left - player1.Left);
                        distances[3] = System.Math.Abs(((PictureBox)x).Top - player1.Top);
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
                            p = player1;
                        }
                    }

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
}
