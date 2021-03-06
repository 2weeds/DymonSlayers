using Microsoft.AspNet.SignalR.Client;
using SgClient1.Builder;
using SgClient1.Classes_Test;
using SgClient1.Decorator;
using SgClient1.Strategy;
using System;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using SgClient1.Visitor;

namespace SgClient1
{
    public partial class FormGame : Form, ICloneable
    {
        bool ultimate = true;
        bool firstLaunch = true;
        public bool goup;
        public bool godown;
        public bool goleft;
        public bool goright;
        public string facing = "up";
        string bulletType = "Fire";
        int zombieCount = 3;
        int score = 0;
        bool fireWallPlaced = false;
        bool gameOver = false;
        Creator lvCreator = new LevelCreator();
        LevelObject level;
        AbstractFactory objectFactory;
        HubConnection _signalRConnection;
        public PlayerClass playerInteractions = new PlayerClass();
        

        NewsReporter reporter = new NewsReporter();

        public Zombie zm = new Zombie();
        IHubProxy _hubProxy;
        ZombieReporter zombieReporter = new ZombieReporter();
        private PlayerReporter playerReporter = new PlayerReporter();
        
        public string group;
        public string userName;
        Random rnd = new Random();
        WeaponDirector pistol = new WeaponDirector(new PistolBuilder());
        WeaponDirector hands = new WeaponDirector(new KillerHandsBuilder());

        LevelStrategy Strategy = null;

        //Decorator
        IceBullet ice;
        FireBullet fire;
        LightningBullet lightning;
        public FormGame(WinFormsClient.FrmClient instance)
        {
            userName = instance.GetName();
            group = instance.GetGroup();
            _signalRConnection = instance._signalRConnection;
            _hubProxy = instance._hubProxy;
            InitializeComponent();
        }

        public FormGame()
        {

        }
        public void clearPbs()
        {
            this.Controls.Remove(pictureBox1);
            pictureBox1.Dispose();
            this.Controls.Remove(pictureBox2);
            pictureBox2.Dispose();
            this.Controls.Remove(pictureBox3);
            pictureBox3.Dispose();
        }
        public void recreateZombies()
        {
            zm.createAZombie(this, pictureBox1.Left, pictureBox1.Top, hands.MakeWeapon().GetWeapon());
            zm.createAZombie(this, pictureBox2.Left, pictureBox2.Top, hands.MakeWeapon().GetWeapon());
            zm.createAZombie(this, pictureBox3.Left, pictureBox3.Top, hands.MakeWeapon().GetWeapon());

        }
        
        public async void Form1_Load(object sender, EventArgs e)
        {
            reporter._IHubProxy = _hubProxy;
            playerInteractions.SetHub(_hubProxy);
            playerInteractions.Health = 100;
            playerInteractions.Attach(reporter);
            recreateZombies();
            clearPbs();

            playerInteractions.Weapon = pistol.MakeWeapon().GetWeapon();

            level = lvCreator.factoryMethod(1);
            objectFactory = level.getAbstractFactory();

            //strategy
            setStrategy(1);

            await asStuf();
            //pictureBox1.Name = zm.UnusedName();
            //pictureBox2.Name = zm.UnusedName();
            //pictureBox3.Name = zm.UnusedName();
            this.Size = new Size(940, 700);
        }

        async Task asStuf()
        {
            _hubProxy.On<int, int>("DropAmmo", (x, y) => dropAmmo(x, y));
            _hubProxy.On<int, int>("SpawnHealthPack", (x, y) => createHealthPack(x, y));
            _hubProxy.On<int, int>("SpawnFireWall", (x, y) => createFireWall(x, y));
            _hubProxy.On<int, int>("SpawnZombie", (x, y) => makeZombies(x, y));
            _hubProxy.On<string>("PlayerDied", (user) => playerDead(user));
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

        private void playerDead(string user)
        {
            if (this.InvokeRequired)//to prevent multiple threads accessing same form or smth idk
            {
                this.Invoke((MethodInvoker)delegate
                {
                    playerDead(user);
                });
                return;
            }
            if (user != userName)
            {
                player1.Image = Properties.Resources.dead;
            }
        }

        public void getMovement(string message, PictureBox player1)
        {
            if (this.InvokeRequired)//to prevent multiple threads accessing same form or smth idk
            {
                this.Invoke((MethodInvoker)delegate
                {
                    getMovement(message, player1);
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

        private void bulletShot(string user, string direct, string bulletType, int bulletLeft, int bulletTop)
        {
            if (this.InvokeRequired)//to prevent multiple threads accessing same form or smth idk
            {
                this.Invoke((MethodInvoker)delegate
                {
                    bulletShot(user, direct, bulletType, bulletLeft, bulletTop);
                });
                return;
            }
            if (user != userName)
            {
                Bullet shoot = new Bullet();
                shoot.direction = direct;
                shoot.bulletLeft = bulletLeft;
                shoot.bulletTop = bulletTop;
                shoot.mkBullet(this, bulletType);
            }
        }

        private void checkAction(string message)
        {
            string[] parts = message.Split(';');
            if (parts[1] == "m")
            {
                getMovement(parts[0] + ";" + parts[2] + ";" + parts[3] + ";" + parts[4], player1);
            }
            else if (parts[1] == "s")
            {
                bulletShot(parts[0], parts[2], parts[3], int.Parse(parts[4]), int.Parse(parts[5]));
            }
        }

        public void keyisdown(object sender, KeyEventArgs e)
        {
            if (gameOver)
            {
                timer1.Stop();
                _hubProxy.Invoke("PlayerDied", group, userName);
                return;
            }

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

            if (e.KeyCode == Keys.Z)
            {
                switch (bulletType)
                {
                    case "Fire":
                        bulletType = "Ice";
                        break;
                    case "Ice":
                        bulletType = "Lightning";
                        break;
                    case "Lightning":
                        bulletType = "Fire";
                        break;
                }
            }
            if (e.KeyCode == Keys.R)
            {
                //Call for ekko ulti
                playerInteractions.careTaker.Undo();
                ultimate = false;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (gameOver)
            {
                timer1.Stop();
                _hubProxy.Invoke("PlayerDied", group, userName);
                return;
            }

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

            if (e.KeyCode == Keys.Space && playerInteractions.ammo > 0)
            {
                playerInteractions.ammo--;
                switch (bulletType)
                {
                    case "Fire":
                        fire.FireShoot(facing);
                        break;
                    case "Ice":
                        ice.IceShoot(facing);
                        break;
                    case "Lightning":
                        lightning.LightningShoot(facing);
                        break;
                }

                if (playerInteractions.ammo < 2)
                {
                    _hubProxy.Invoke("UpdateBullets", group, rnd.Next(10, 790), rnd.Next(50, 500));
                }
            }

            if (e.KeyCode == Keys.C)
            {
                //deep
                var copyD = copyDeep();
                var addressDeep = copyD.GetHashCode();
                var copyS = copyShallow();
                var addressShallow = copyS.GetHashCode();
                //_hubProxy.Invoke("Send", String.Format("{0} {1}", "Deep copy address", addressDeep));
                //_hubProxy.Invoke("Send", String.Format("{0} {1}", "Shallow copy address", addressShallow));
                //_hubProxy.Invoke("Send", String.Format("{0} {1}", "Real address", this.GetHashCode()));
                //_hubProxy.Invoke("Send", String.Format("{0} {1} {2}", "Deep player1 and player2 coordinates", copyD.player.Location, copyD.player1.Location));
                //_hubProxy.Invoke("Send", String.Format("{0} {1} {2}", "Shallow player1 and player2 coordinates", copyS.player.Location, copyS.player1.Location));
                //_hubProxy.Invoke("Send", String.Format("{0} {1} {2}", "Real player1 and player2 coordinates", this.player.Location, this.player1.Location));
                //_hubProxy.Invoke<string, string>("AddMessage", (name, message) => checkAction($"{name};{message}"));
                //_hubProxy.Invoke("SaveGame", group, player.Location, player1.Location);
            }
        }
        public PictureBox GetPlayer() { return player; }
        public PictureBox GetPlayer1() { return player1; }

        private void gameEngine(object sender, EventArgs e)
        {
            if (firstLaunch)
            {
                playerInteractions.form = this;
                playerInteractions._hubProxy = _hubProxy;
                playerInteractions.group = group;
                playerInteractions.player = player;
                playerInteractions.player1 = player1;

                zm.form = this;
                zm._hubProxy = _hubProxy;
                zm.group = group;
                zm.player = player;
                zm.player1 = player1;

                firstLaunch = false;

                //Decorator
                fire = new FireBullet(playerInteractions);
                ice = new IceBullet(playerInteractions);
                lightning = new LightningBullet(playerInteractions);
            }

            if (score == 10)
            {
                level = lvCreator.factoryMethod(2);
                objectFactory = level.getAbstractFactory();
                //strategy
                setStrategy(2);
            }
            else if (score == 20)
            {
                level = lvCreator.factoryMethod(3);
                objectFactory = level.getAbstractFactory();
                //strategy
                setStrategy(3);
            }
            if (score % 10 == 0 && score != 0)
            {
                if (fireWallPlaced == false)
                {
                    _hubProxy.Invoke("UpdateFireWalls", group, rnd.Next(10, 790), rnd.Next(50, 500));
                    fireWallPlaced = true;
                }
            }

            label1.Text = " Ammo: " + playerInteractions.ammo;
            label2.Text = "Kills: " + score;

            gameOver = playerInteractions.playerGameEngine(progressBar1, goleft, goup, goright, godown);
            initializeZombieInteractions(zm);

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Name == "bulletF" || x is PictureBox && x.Name == "bulletI" || x is PictureBox && x.Name == "bulletL")//if bullet leaves map
                {
                    if (((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 930 || ((PictureBox)x).Top < 10 || ((PictureBox)x).Top > 700)
                    {
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                    }
                }

                foreach (Control j in this.Controls)
                {
                    if ((j is PictureBox && j.Name == "bulletF" || j is PictureBox && j.Name == "bulletI" || j is PictureBox && j.Name == "bulletL") && (x is PictureBox && zm.ContainsName(x.Name)))
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))//if bullet intercepts zombie (j is bullet) 
                        {
                            Zombie z = zm.Find(x.Name);
                            if (z.Health < 2) //zombie dies if bullet hits and health is 1 or less
                            {
                                score++;
                                fireWallPlaced = false;
                                zombieCount--;
                                this.Controls.Remove(x);
                                x.Dispose();
                                zm.RemoveZombie(z);
                                if (zombieCount < 3)
                                {
                                    _hubProxy.Invoke("UpdateZombies", group, rnd.Next(10, 790), rnd.Next(50, 500));
                                }
                            }
                            else//  zombie takes dmg
                            {
                                playerInteractions.DoDamage(z, x);
                            }
                            //remove bullet
                            this.Controls.Remove(j);
                            j.Dispose();
                        }
                    }
                }
            }
        }

        private void initializeZombieInteractions(Zombie zmb)
        {
            if (player.Image == Properties.Resources.dead && player1.Image != Properties.Resources.dead)
            {
                zmb.zombieInteractions(playerInteractions, 1);
            }
            else if (player.Image != Properties.Resources.dead && player1.Image == Properties.Resources.dead)
            {
                zmb.zombieInteractions(playerInteractions, 0);
            }
            else
                zmb.zombieInteractions(playerInteractions, 2);
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
            mapObj.spawnUnit(this, x, y);
            player.BringToFront();
            playerReporter._IHubProxy = _hubProxy;
            playerReporter.ReportEntity(playerInteractions.ReportToServer());
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
            mapObj.spawnUnit(this, x, y);
            player.BringToFront();
            playerReporter._IHubProxy = _hubProxy;
            playerReporter.ReportEntity(playerInteractions.ReportToServer());
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
            mapObj.spawnUnit(this, x, y);
            player.BringToFront();
            playerReporter._IHubProxy = _hubProxy;
            playerReporter.ReportEntity(playerInteractions.ReportToServer());
        }

        public void makeZombies(int x, int y)
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
                zm.createAZombie(this, x, y, hands.MakeWeapon().GetWeapon());
                zombieReporter._IHubProxy = _hubProxy;
                zombieReporter.ReportEntity(this.zm.ReportToServer());
                zombieCount++;
            }
        }

        public void setStrategy(int level)
        {
            if (level == 1)
            {
                playerInteractions.level = 1;
                Strategy = new FirstLevelStrategy();
            }
            else if (level == 2)
            {
                playerInteractions.level = 2;
                Strategy = new SecondLevelStrategy();
            }
            else if (level == 3)
            {
                playerInteractions.level = 3;
                Strategy = new ThirdLevelStrategy();
            }

            Strategy.action(playerInteractions, zm);
        }

        public FormGame copyShallow()
        {
            return (FormGame)Clone();
        }

        public FormGame copyDeep()
        {
            var copy = new FormGame();
            copy.userName = Name;
            copy.group = group;
            copy._signalRConnection = _signalRConnection;
            copy._hubProxy = _hubProxy;
            copy.player = player;
            copy.player1 = player1;

            return copy;
        }

        public object Clone()
        {
            return (FormGame)this;
        }

    }
}
