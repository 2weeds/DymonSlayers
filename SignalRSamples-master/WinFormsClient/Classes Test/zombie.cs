using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SgClient1.Classes_Test
{
    class Zombie : GameClass
    {
        Random rnd = new Random();
        public int speed = 3;
        PictureBox zombie = new PictureBox();
        public int zombieLeft;
        public int zombieTop;

        public void zombieInteractions(ref double playerHealth, int chaseCase)
        {
            foreach (Control x in form.Controls)
            {
                if (x is PictureBox && x.Name == "zombie")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealth -= 1;
                        if (playerHealth == 30 || playerHealth == 20)
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

        public void createAZombie(FormGame form)
        {
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombie.Name = "zombie";
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = zombieLeft;
            zombie.Top = zombieTop;
            zombie.BringToFront();
            form.Controls.Add(zombie);
        }
    }
}
