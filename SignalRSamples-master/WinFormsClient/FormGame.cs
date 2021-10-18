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
        int zombieCount = 3;
        int score = 0;
        bool fireWallPlaced = false;
        bool gameOver = false;
        Creator lvCreator = new LevelCreator();
        LevelObject level;
        AbstractFactory objectFactory;
        HubConnection _signalRConnection;
        IHubProxy _hubProxy;
        string group;
        string userName;
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
            level = lvCreator.factoryMethod(1);
            objectFactory = level.getAbstractFactory();
            await asStuf();
            pictureBox1.Name = "zombie";
            pictureBox2.Name = "zombie";
            this.Size = new Size(940, 700);
        }

        async Task asStuf()
        {
            _hubProxy.On<int,int>("DropAmmo", (x, y) => dropAmmo(x, y));
            _hubProxy.On<int, int>("SpawnHealthPack", (x, y) => createHealthPack(x, y));
            _hubProxy.On<int, int>("SpawnFireWall", (x, y) => createFireWall(x, y));
            _hubProxy.On<int, int>("SpawnZombie", (x, y) => makeZombies(x, y));
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
            string direction = parts[1];
            string x = parts[2];
            string y = parts[3];
            if (sender != userName)
            {
                if (direction == "left")
                {
                    player1.Image = Properties.Resources.left1;
                }
                else if (direction == "right")
                {
                    player1.Image = Properties.Resources.right1;
                }
                else if (direction == "up")
                {
                    player1.Image = Properties.Resources.up1;
                }
                else if (direction == "down")
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
                getMovement(parts[0] + ";" + parts[2] + ";" + parts[3] + ";" + parts[4]);
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

                if (ammo < 2)
                {
                    _hubProxy.Invoke("UpdateBullets", group, rnd.Next(10, 790), rnd.Next(50, 500));
                }
            }
        }

        private void gameEngine(object sender, EventArgs e)
        {
            if (score == 10)
            {
                level = lvCreator.factoryMethod(2);
                objectFactory = level.getAbstractFactory();
            } else if (score == 20)
            {
                level = lvCreator.factoryMethod(3);
                objectFactory = level.getAbstractFactory();
            }
            if (score % 10 == 0 && score != 0)
            {
                if (fireWallPlaced == false)
                {
                    _hubProxy.Invoke("UpdateFireWalls", group, rnd.Next(10, 790), rnd.Next(50, 500));
                    fireWallPlaced = true;
                }
            }
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

            foreach (Control x in this.Controls)
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
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                        if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds)){ammo += ammoAdd;}
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
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                        if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds)) { playerHealth += healAmount; if (playerHealth > 100) { playerHealth = 100; } }
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
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                        if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds)) { playerHealth -= damage; }
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
                   /* if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealth -= 1;
                        if (playerHealth == 30 || playerHealth == 20)
                        {
                            _hubProxy.Invoke("UpdateHealthPacks", group, rnd.Next(10, 790), rnd.Next(50, 500));
                        }
                    } */
                    var p = player;
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

                    if (((PictureBox)x).Left > p.Left)
                    {
                        ((PictureBox)x).Left -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }

                    if (((PictureBox)x).Left < p.Left)
                    {
                        ((PictureBox)x).Left += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }

                    if (((PictureBox)x).Top > p.Top)
                    {
                        ((PictureBox)x).Top -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }

                    if (((PictureBox)x).Top < p.Top)
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
                            fireWallPlaced = false;
                            zombieCount--;
                            this.Controls.Remove(j);
                            j.Dispose();
                            this.Controls.Remove(x);
                            x.Dispose();
                            if (zombieCount < 3)
                            {
                                _hubProxy.Invoke("UpdateZombies", group, rnd.Next(10, 790), rnd.Next(50, 500));
                            }
                        }
                    }
                }
            }
        }

        private void dropAmmo(int x, int y)
        {
            if (this.InvokeRequired)//to prevent multiple threads accessing same form or smth idk
            {
                this.Invoke((MethodInvoker)delegate
                {
                    dropAmmo(x, y);
                });
                return;
            }
            Unit mapObj = objectFactory.createGunBullets();
            mapObj.updateState(this, x, y);
            player.BringToFront();
        }

        private void createHealthPack(int x, int y)
        {
            if (this.InvokeRequired)//to prevent multiple threads accessing same form or smth idk
            {
                this.Invoke((MethodInvoker)delegate
                {
                    createHealthPack(x, y);
                });
                return;
            }
            Unit mapObj = objectFactory.createHealKit();
            mapObj.updateState(this, x, y);
            player.BringToFront();
        }

        private void createFireWall(int x, int y)
        {
            if (this.InvokeRequired)//to prevent multiple threads accessing same form or smth idk
            {
                this.Invoke((MethodInvoker)delegate
                {
                    createFireWall(x, y);
                });
                return;
            }
            Unit mapObj = objectFactory.createFireWall();
            mapObj.updateState(this, x, y);
            player.BringToFront();
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

        private void makeZombies(int x, int y)
        {
            if (this.InvokeRequired)//to prevent multiple threads accessing same form or smth idk
            {
                this.Invoke((MethodInvoker)delegate
                {
                    makeZombies(x, y);
                });
                return;
            }
            if (zombieCount < 3)
            {
                zombie zm = new zombie();
                zm.zombieLeft = x;
                zm.zombieTop = y;
                zm.createAZombie(this);
                zombieCount++;
            }
        }
    }
}
