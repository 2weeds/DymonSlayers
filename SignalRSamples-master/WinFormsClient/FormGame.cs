using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SgClient1
{
    public partial class FormGame : Form
    {
        bool goup;
        bool godown;
        bool goleft;
        bool goright;
        string facing = "up";
        double playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int zombieSpeed = 3;
        int score = 0;
        bool gameOver = false;
        HubConnection _signalRConnection;
        IHubProxy _hubProxy;
        string group;
        string userName;
       /* PictureBox player = new PictureBox();
        PictureBox player1 = new PictureBox(); */
        Random rnd = new Random();

        public FormGame(WinFormsClient.FrmClient instance)
        {
            userName = instance.GetName();
            group = instance.GetGroup();
            _signalRConnection = instance._signalRConnection;
            _hubProxy = instance._hubProxy;
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await asStuf();
            /* for (int i = 0; i < 2; i++)
             {
                 makeZombies();
                 this.Size = new Size(940, 700);
             } */
        }

        async Task asStuf()
        {
            _hubProxy.On<int>("DropAmmo", (ammo) => dropAmmo(ammo));
            _hubProxy.On<string, string>("AddMessage", (name, message) => checkAction($"{name};{message}"));
            try
            {
                await _hubProxy.Invoke("UpdateSpawns", group);
            }
            catch (Exception ex)
            {
                //Spawn($"EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE:  {ex}"); // old method to spawn labels
                throw;
            }

        }

        /// <summary>
        /// Spawns whole group (2 players) to map
        /// </summary>
        /// <param name="ids"> IDs of players in group list </param>
        void SpawnGroup(List<string> ids)
        {
            if (this.InvokeRequired)//to prevent multiple threads accessing same form or smth idk
            {
                this.Invoke((MethodInvoker)delegate
                {
                    SpawnGroup(ids);
                });
                return;
            }

          /*  player.Name = "player1";
            player.Location = new Point(350, 190);
            player.Visible = true;
            Controls.Add(player);
            player.BringToFront();

            ids.Remove(_signalRConnection.ConnectionId);
            player1.Name = "player2";
            player1.Location = new Point(350, 190);
            player1.Visible = true;
            Controls.Add(player1);
            player1.BringToFront(); */
            
        }

        private void getMovement(string message)
        {
            if (this.InvokeRequired)//to prevent multiple threads accessing same form or smth idk
            {
                this.Invoke((MethodInvoker)delegate
                {
                    getMovement(message);
                });
                return;
            }
            string[] parts = message.Split(';');
            string sender = parts[0];
            string x = parts[1];
            string y = parts[2];
            if (sender != userName)
            {
                if (player1.Location.X < int.Parse(x))
                {
                    player1.Image = Properties.Resources.right1;
                }
                else if (player1.Location.X > int.Parse(x))
                {
                    player1.Image = Properties.Resources.left1;
                }
                else if (player1.Location.Y > int.Parse(y))
                {
                    player1.Image = Properties.Resources.up1;
                }
                else if (player1.Location.Y < int.Parse(y))
                {
                    player1.Image = Properties.Resources.down1;
                }
                player1.Location = new Point(int.Parse(x), int.Parse(y));
            }
        }

        private void bulletShot(string user, string direct, int bulletLeft, int bulletTop)
        {
            if (this.InvokeRequired)//to prevent multiple threads accessing same form or smth idk
            {
                this.Invoke((MethodInvoker)delegate
                {
                    bulletShot(user, direct, bulletLeft, bulletTop);
                });
                return;
            }
            if (user != userName)
            {
                bullet shoot = new bullet();
                shoot.direction = direct;
                shoot.bulletLeft = bulletLeft;
                shoot.bulletTop = bulletTop;
                shoot.mkBullet(this, "Red");
            }
        }

        private void checkAction(string message)
        {
            string[] parts = message.Split(';');
            if (parts[1] == "m")
            {
                getMovement(parts[0] + ";" + parts[2] + ";" + parts[3]);
            } else if (parts[1] == "s")
            {
                bulletShot(parts[0], parts[2], int.Parse(parts[3]), int.Parse(parts[4]));
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (gameOver) return;

            if (e.KeyCode == Keys.A)
            {
                goleft = true;
                facing = "left";
                player.Image = Properties.Resources.left;
            }

            if (e.KeyCode == Keys.D)
            {
                goright = true;
                facing = "right";
                player.Image = Properties.Resources.right;
            }

            if (e.KeyCode == Keys.W)
            {
                goup = true;
                facing = "up";
                player.Image = Properties.Resources.up;
            }

            if (e.KeyCode == Keys.S)
            {
                godown = true;
                facing = "down";
                player.Image = Properties.Resources.down;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (gameOver) return;

            if (e.KeyCode == Keys.A)
            {
                goleft = false;
            }

            if (e.KeyCode == Keys.D)
            {
                goright = false;
            }

            if (e.KeyCode == Keys.W)
            {
                goup = false;
            }

            if (e.KeyCode == Keys.S)
            {
                godown = false;
            }

            if (e.KeyCode == Keys.Space && ammo > 0)
            {
                ammo--;
                shoot(facing);

                //if (ammo < 2)
                //{
                //    dropAmmo();
                //}
                _hubProxy.Invoke("UpdateBullets",group, ammo);
            }
        }

        private void gameEngine(object sender, EventArgs e)
        {
            if (playerHealth > 1)
            {
                progressBar1.Value = Convert.ToInt32(playerHealth);
            }
            else
            {
                player.Image = Properties.Resources.dead;
                timer1.Stop();
                gameOver = true;
            }

            label1.Text = " Ammo: " + ammo;
            label2.Text = "Kills: " + score;

            if (playerHealth < 20)
            {
                progressBar1.ForeColor = System.Drawing.Color.Red;
            }

            if (goleft && player.Left > 0)
            {
                player.Left -= speed;
            }

            if (goright && player.Left + player.Width < 930)
            {
                player.Left += speed;
            }

            if (goup && player.Top > 60)
            {
                player.Top -= speed;
            }

            if (godown && player.Top + player.Height < 700)
            {
                player.Top += speed;
            }
            _hubProxy.Invoke("Send", $"m;{player.Location.X};{player.Location.Y}");

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Name == "ammo")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        this.Controls.Remove(((PictureBox)x));

                        ((PictureBox)x).Dispose();
                        ammo += 5;
                    }
                }

                if (x is PictureBox && x.Name == "bullet")
                {
                    if (((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 930 || ((PictureBox)x).Top < 10 || ((PictureBox)x).Top > 700)
                    {
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                    }
                }

                if (x is PictureBox && x.Name == "zombie")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealth -= 1;
                    }

                    if (((PictureBox)x).Left > player.Left)
                    {
                        ((PictureBox)x).Left -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }

                    if (((PictureBox)x).Left < player.Left)
                    {
                        ((PictureBox)x).Left += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }

                    if (((PictureBox)x).Top > player.Top)
                    {
                        ((PictureBox)x).Top -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }

                    if (((PictureBox)x).Top < player.Top)
                    {
                        ((PictureBox)x).Top += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }
                }

                foreach (Control j in this.Controls)
                {
                    if ((j is PictureBox && j.Name == "bullet") && (x is PictureBox && x.Name == "zombie"))
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;
                            this.Controls.Remove(j);
                            j.Dispose();
                            this.Controls.Remove(x);
                            x.Dispose();
                            // makeZombies();
                        }
                    }
                }
            }
        }

        private void dropAmmo(int ammosize)
        {
            if (this.InvokeRequired)//to prevent multiple threads accessing same form or smth idk
            {
                this.Invoke((MethodInvoker)delegate
                {
                    dropAmmo(ammosize);
                });
                return;
            }
            if (ammosize<2)
            {
                PictureBox ammo = new PictureBox();
                ammo.Image = Properties.Resources.ammo_Image;
                ammo.SizeMode = PictureBoxSizeMode.AutoSize;
                ammo.Left = rnd.Next(10, 790);
                ammo.Top = rnd.Next(50, 500);
                ammo.Name = "ammo";
                this.Controls.Add(ammo);
                ammo.BringToFront();
                player.BringToFront();
            }
        }

        private void shoot(string direct)
        {
            bullet shoot = new bullet();
            shoot.direction = direct;
            shoot.bulletLeft = player.Left + (player.Width / 2);
            shoot.bulletTop = player.Top + (player.Height / 2);
            shoot.mkBullet(this, "White");
            _hubProxy.Invoke("Send", $"s;{direct};{player.Left + (player.Width / 2)};{player.Top + (player.Height / 2)}");

        }

        private void makeZombies()
        {
            PictureBox zombie = new PictureBox();
            zombie.Name = "zombie";
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = rnd.Next(0, 900);
            zombie.Top = rnd.Next(0, 800);
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Controls.Add(zombie);
            player.BringToFront();
        }
    }
}
