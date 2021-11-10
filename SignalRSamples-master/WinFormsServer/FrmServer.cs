using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsServer
{
    public partial class FrmServer : Form
    {
        private IDisposable _signalR;
        private BindingList<ClientItem> _clients = new BindingList<ClientItem>();
        private BindingList<string> _groups = new BindingList<string>();
        private ConcurrentDictionary<string, int> _groupsCount = new ConcurrentDictionary<string, int>();
        private ConcurrentDictionary<string, int> _readyCount = new ConcurrentDictionary<string, int>();

        public FrmServer()
        {
            InitializeComponent();

            bindListsToControls();

            //Register to static hub events
            SimpleHub.ClientConnected += SimpleHub_ClientConnected;
            SimpleHub.ClientDisconnected += SimpleHub_ClientDisconnected;
            SimpleHub.ClientNameChanged += SimpleHub_ClientNameChanged;
            SimpleHub.ClientJoinedToGroup += SimpleHub_ClientJoinedToGroup;
            SimpleHub.ClientReadyCheck += SimpleHub_ClientReadyCheck;
            SimpleHub.ResetClientReadyCheck += SimpleHub_ResetClientReadyCheck;
            SimpleHub.ClientLeftReady += SimpleHub_ClientLeftReady;
            SimpleHub.ClientLeftGroup += SimpleHub_ClientLeftGroup;
            SimpleHub.MessageReceived += SimpleHub_MessageReceived;

            SimpleHub.UpdateSpawn += SimpleHub_UpdateSpawn;
            SimpleHub.OutOfBullets += SimpleHub_OutOfBullets;
            SimpleHub.OutOfZombies += SimpleHub_OutOfZombies;
            SimpleHub.DropHealthPack += SimpleHub_DropHealthPack;
            SimpleHub.DropFireWall += SimpleHub_CreateFireWall;
            SimpleHub.DeadPlayer += SimpleHub_ShowPlayerDeath;
        }

        public void bindListsToControls()
        {
            //Clients list
            cmbClients.DisplayMember = "Name";
            cmbClients.ValueMember = "Id";
            cmbClients.DataSource = _clients;

            //Groups list
            cmbGroups.DataSource = _groups;
        }

        public void SimpleHub_ClientConnected(string clientId)
        {
            if (IsHandleCreated)
            {
                this.BeginInvoke(new Action(() => _clients.Add(new ClientItem() { Id = clientId, Name = clientId })));
                writeToLog($"Client connected:{clientId}");
            }
            //Add client to our clients list
            else
            {
                return;
            }
        }

        public void SimpleHub_ClientDisconnected(string clientId)
        {
            if (!IsHandleCreated) return;
            this.BeginInvoke(new Action(() =>
            {
                var client = _clients.FirstOrDefault(x => x.Id == clientId);
                if (client != null)
                    _clients.Remove(client);
            }));

            writeToLog($"Client disconnected:{clientId}");
            //Remove client from the list
        }

        private void SimpleHub_ClientNameChanged(string clientId, string newName)
        {
            //Update the client's name if it exists
            this.BeginInvoke(new Action(() =>
            {
                var client = _clients.FirstOrDefault(x => x.Id == clientId);
                if (client != null)
                    client.Name = newName;
            }));

            writeToLog($"Client name changed. Id:{clientId}, Name:{newName}");
        }

        private void SimpleHub_ClientJoinedToGroup(string clientId, string groupName)
        {
            //Only add the groups name to our groups list
            this.BeginInvoke(new Action(() =>
            {
                int groupSize;
                var group = _groups.FirstOrDefault(x => x == groupName);
                if (group == null)
                {
                    _groups.Add(groupName);
                    _groupsCount.TryAdd(groupName, +1);
                }
                else
                {
                    _groupsCount.AddOrUpdate(groupName, 1, (groupname, count) => count + 1);
                }
                //_groupsCount.TryGetValue(groupName, out groupSize);
                _groupsCount.TryGetValue(groupName, out groupSize);
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
                hubContext.Clients.Group(groupName).updateMembers(groupSize);

            }));
            writeToLog($"Client joined to group. Id:{clientId}, Group:{groupName}");
        }

        private void SimpleHub_ClientReadyCheck(string clientId, string groupName)
        {
            this.BeginInvoke(new Action(() =>
            {
                int groupSize;
                var group = _groups.FirstOrDefault(x => x == groupName);
                if (group == null)
                {
                    _readyCount.TryAdd(groupName, +1);
                }
                else
                {
                    _readyCount.AddOrUpdate(groupName, 1, (groupname, count) => count + 1);
                }
                //_groupsCount.TryGetValue(groupName, out groupSize);
                _readyCount.TryGetValue(groupName, out groupSize);
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
                hubContext.Clients.Group(groupName).updateReady(groupSize);
                hubContext.Clients.Group(groupName).getReadyPlayers(groupSize);
                //writeToLog($"Ready count: :{groupSize}");
            }));
            writeToLog($"Client is ready. Id:{clientId}, Group:{groupName}");

        }

        private void SimpleHub_ResetClientReadyCheck(string clientId, string groupName)
        {
            this.BeginInvoke(new Action(() =>
            {
                int lastReadySize;
                _readyCount.TryRemove(groupName, out lastReadySize);
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
                hubContext.Clients.Group(groupName).updateReady(0);
            }));
            writeToLog($"Resetting ready checks. Initiated by client: Id:{clientId}, Group:{groupName}");
        }

        private void SimpleHub_ClientLeftGroup(string clientId, string groupName)
        {
            this.BeginInvoke(new Action(() =>
            {
                int groupSize;
                int checkSize;
                var group = _groups.FirstOrDefault(x => x == groupName);
                _groupsCount.TryGetValue(groupName, out groupSize);
                if (group != null && groupSize > 1)
                {
                    _groupsCount.AddOrUpdate(groupName, 1, (groupname, count) => count - 1);
                    _readyCount.TryGetValue(groupName, out checkSize);
                    if (checkSize > 0)
                    {
                        _readyCount.AddOrUpdate(groupName, 1, (groupname, count) => count - 1);
                    }
                    _groupsCount.TryGetValue(groupName, out groupSize);
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
                    hubContext.Clients.Group(groupName).updateMembers(groupSize);
                }
                else if (group != null && groupSize == 1)
                {
                    int lastSize;
                    int lastReadySize;
                    _groups.Remove(groupName);
                    _groupsCount.TryRemove(groupName, out lastSize);
                    _readyCount.TryRemove(groupName, out lastReadySize);
                }

            }));
            writeToLog($"Client left group. Id:{clientId}, Group:{groupName}");
        }

        private void SimpleHub_ClientLeftReady(string clientId, string groupName)
        {
            this.BeginInvoke(new Action(() =>
            {
                int groupSize;
                int checkSize;
                // var group = _groups.FirstOrDefault(x => x == groupName);
                _readyCount.TryGetValue(groupName, out groupSize);
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
                if (/*group != null && */groupSize > 1)
                {
                    _readyCount.AddOrUpdate(groupName, 1, (groupname, count) => count - 1);
                    _readyCount.TryGetValue(groupName, out groupSize);
                }
                else if (/*group != null && */groupSize == 1)
                {
                    int lastSize;
                    int lastReadySize;
                    _readyCount.AddOrUpdate(groupName, 1, (groupname, count) => count - 1);
                    _readyCount.TryGetValue(groupName, out groupSize);
                    // _readyCount.TryRemove(groupName, out groupSize);
                }

                hubContext.Clients.Group(groupName).updateReady(groupSize);
                hubContext.Clients.Group(groupName).getReadyPlayers(groupSize);
            }));
            writeToLog($"Client left ready check. Id:{clientId}, Group:{groupName}");
        }

        private void SimpleHub_MessageReceived(string senderClientId, string message)
        {
            //One of the clients sent a message, log it
            this.BeginInvoke(new Action(() =>
            {
                string clientName = _clients.FirstOrDefault(x => x.Id == senderClientId)?.Name;

                writeToLog($"{clientName}:{message}");
            }));
        }

        private void SimpleHub_UpdateSpawn(string clientId, string groupName)
        {
            this.BeginInvoke(new Action(() =>
            {
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
                hubContext.Clients.Group(groupName).spawnPlayer(clientId);
            }));
        }

        private void SimpleHub_OutOfBullets(string clientId, string groupName, int x, int y)
        {
            this.BeginInvoke(new Action(() =>
            {
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
                hubContext.Clients.Group(groupName).DropAmmo(x, y);
            }));
        }

        private void SimpleHub_OutOfZombies(string clientId, string groupName, int x, int y)
        {
            this.BeginInvoke(new Action(() =>
            {
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
                hubContext.Clients.Group(groupName).SpawnZombie(x, y);
            }));
        }

        private void SimpleHub_DropHealthPack(string clientId, string groupName, int x, int y)
        {
            this.BeginInvoke(new Action(() =>
            {
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
                hubContext.Clients.Group(groupName).SpawnHealthPack(x, y);
            }));
        }

        private void SimpleHub_CreateFireWall(string clientId, string groupName, int x, int y)
        {
            this.BeginInvoke(new Action(() =>
            {
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
                hubContext.Clients.Group(groupName).SpawnFireWall(x, y);
            }));
        }

        private void SimpleHub_ShowPlayerDeath(string clientId, string groupName, string userName)
        {
            this.BeginInvoke(new Action(() =>
            {
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
                hubContext.Clients.Group(groupName).PlayerDied(userName);
            }));
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            txtLog.Clear();

            try
            {
                //Start SignalR server with the give URL address
                //Final server address will be "URL/signalr"
                //Startup.Configuration is called automatically
                _signalR = WebApp.Start<Startup>(txtUrl.Text);

                btnStartServer.Enabled = false;
                txtUrl.Enabled = false;
                btnStop.Enabled = true;
                grpBroadcast.Enabled = true;

                writeToLog($"Server started at:{txtUrl.Text}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _clients.Clear();
            _groups.Clear();

            SimpleHub.ClearState();

            if (_signalR != null)
            {
                _signalR.Dispose();
                _signalR = null;

                btnStop.Enabled = false;
                btnStartServer.Enabled = true;
                txtUrl.Enabled = true;
                grpBroadcast.Enabled = false;

                writeToLog("Server stopped.");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();

            if (rdToAll.Checked)
            {
                hubContext.Clients.All.addMessage("SERVER", txtMessage.Text);
            }
            else if (rdToGroup.Checked)
            {
                hubContext.Clients.Group(cmbGroups.Text).addMessage("SERVER", txtMessage.Text);
            }
            else if (rdToClient.Checked)
            {
                hubContext.Clients.Client((string)cmbClients.SelectedValue).addMessage("SERVER", txtMessage.Text);
            }
        }

        private void writeToLog(string log)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(() => txtLog.AppendText(log + Environment.NewLine)));
            else
                txtLog.AppendText(log + Environment.NewLine);
        }

        private void FrmServer_Load(object sender, EventArgs e)
        {

        }
    }
}
