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
    public partial class FrmTest : Form
    {
        //Dictionary<string, Label> players = new Dictionary<string, Label>(4);//2 cuz 2 playaz
        Label p1 = new Label();
        Label p2 = new Label();
        //Connection to a SignalR server
        HubConnection _signalRConnection;
        //Proxy object for a hub hosted on the SignalR server
        IHubProxy _hubProxy;
        string group;
        public FrmTest(HubConnection hc, IHubProxy hp, string gid)
        {
            group = gid;
            _signalRConnection = hc;
            _hubProxy = hp;
            InitializeComponent();
        }

        private async void FrmTest_Load(object sender, EventArgs e)
        {
            //Spawn("HELLO");
            await asStuf();
        }

        async Task asStuf()
        {
            _hubProxy.On<string>("spawnPlayer", (name) => Spawn(name));
            _hubProxy.On<string, string>("AddMessage", (name, message) => getMovement($"{name};{message}"));
            try
            {
                await _hubProxy.Invoke("UpdateSpawns", group);
            }
            catch (Exception ex)
            {
                Spawn($"EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE:  {ex}");
                throw;
            }

        }
        void Spawn(string name)
        {
            if (this.InvokeRequired)//to prevent multiple threads accessing same form
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Spawn(name);
                });
                return;
            }
            if (name == _signalRConnection.ConnectionId)
            {
                p1.Text = _signalRConnection.ConnectionId;
                p1.Location = new Point(150, 90);
                p1.Visible = true;
                p1.BackColor = Color.Green;
                Controls.Add(p1);
                p1.BringToFront();
            }
            if (name != _signalRConnection.ConnectionId)
            {
                p2.Text = name;
                p2.Location = new Point(150, 95);
                p2.Visible = true;
                p2.BackColor = Color.Blue;
                Controls.Add(p2);
                p2.BringToFront();
            }

        }

        //simple movement (diaognal movement dont work for now)
        private void FrmTests_KeyDown(object sender, KeyEventArgs e)
        {
            Keys[] keys = { Keys.W, Keys.A, Keys.S, Keys.D };
            int[] dxs = { 0, -5, 0, 5 };
            int[] dys = { -5, 0, 5, 0 };
            if (keys.Contains(e.KeyData))
            {
                int i = Array.IndexOf(keys, e.KeyData);

                string id = _signalRConnection.ConnectionId;

                p1.Location = new Point(p1.Location.X + dxs[i], p1.Location.Y + dys[i]);

                //_hubProxy.Invoke("Send", $"{p1.Text}, {p1.Location.X}, {p1.Location.Y}");
                _hubProxy.Invoke("Send", $"{p1.Location.X};{p1.Location.Y}");
            }
        }

        private void getMovement(string message)
        {
            string[] parts = message.Split(';');
            string sender = parts[0];
            string x = parts[1];
            string y = parts[2];
            if (sender != _signalRConnection.ConnectionId)
            {
                p2.Location = new Point(int.Parse(x), int.Parse(y));
            }
        }
    }
}
