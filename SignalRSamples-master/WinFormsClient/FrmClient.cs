using Microsoft.AspNet.SignalR.Client;
using SgClient1;
using SgClient1.Strategy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WinFormsClient
{
    public partial class FrmClient : Form
    {
        //Connection to a SignalR server
        public HubConnection _signalRConnection;
        //Proxy object for a hub hosted on the SignalR server
        public IHubProxy _hubProxy;
        public static FrmClient instance;
        public FrmClient()
        {
            instance = this;
            InitializeComponent();
        }
        public FrmClient getInstance() { return instance; }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            await connectAsync();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            ClientOptionsAlgorithm Disconnect = new DisconnectFromServer();
            Disconnect.action(ref _signalRConnection, ref _hubProxy, instance);
            //Close the server connection if exists
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            ClientOptionsAlgorithm Send = new SendToTheHub();
            Send.action(ref _signalRConnection, ref _hubProxy, instance);
            //Call the "Send" method on the hub (on the server) with the given parameters
        }

        private void btnJoinGroup_Click(object sender, EventArgs e)
        {
            ClientOptionsAlgorithm JoinGroup = new JoinGroup();
            JoinGroup.action(ref _signalRConnection, ref _hubProxy, instance);
            //Call the "JoinGroup" method on the hub (on the server)
        }

        private void btnLeaveGroup_Click(object sender, EventArgs e)
        {
            ClientOptionsAlgorithm LeaveGroup = new LeaveGroup();
            LeaveGroup.action(ref _signalRConnection, ref _hubProxy, instance);
            //Call the "LeaveGroup" method on the hub (on the server)
        }

        private async Task connectAsync()
        {
            //Create a connection for the SignalR server
            _signalRConnection = new HubConnection(txtUrl.Text);
            _signalRConnection.StateChanged += HubConnection_StateChanged;

            //Get a proxy object that will be used to interact with the specific hub on the server
            //Ther may be many hubs hosted on the server, so provide the type name for the hub
            _hubProxy = _signalRConnection.CreateHubProxy("SimpleHub");

            //Reigster to the "AddMessage" callback method of the hub
            //This method is invoked by the hub
            _hubProxy.On<string, string>("AddMessage", (name, message) => writeToLog($"{name}:{message}"));
            _hubProxy.On<int>("updateMembers", (size) => updateGroupSize(size));
            _hubProxy.On<int>("updateReady", (size) => updateReadyCheckSize(size));
            _hubProxy.On<int>("getReadyPlayers", (size) => triggerGameForm(size));

            btnConnect.Enabled = false;

            try
            {
                //Connect to the server
                await _signalRConnection.Start();

                //Send user name for this client, so we won't need to send it with every message
                await _hubProxy.Invoke("SetUserName", txtUserName.Text);

                txtUrl.Enabled = false;
                txtUserName.Enabled = false;
                btnDisconnect.Enabled = true;
                grpMessaging.Enabled = true;
                grpMembership.Enabled = true;
                grpServer1.Enabled = true;
            }
            catch (Exception ex)
            {
                writeToLog($"Error:{ex.Message}");
                btnConnect.Enabled = true;
            }
        }

        public void HubConnection_StateChanged(StateChange obj)
        {
            if (obj.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
                writeToLog("Connected");
            else if (obj.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
                writeToLog("Disconnected");
        }

        public void writeToLog(string log)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(() => txtLog.AppendText(log + Environment.NewLine)));
            else
                txtLog.AppendText(log + Environment.NewLine);
        }

        public void triggerGameForm(int size)
        {
            if (size == 2)
            {
                // FormGame gamefrm = new FormGame();
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() => btnPlay.Visible = true));
                    this.BeginInvoke(new Action(() => readyServerButton1.Visible = false));
                }
                else
                {
                    btnPlay.Visible = true;
                    readyServerButton1.Visible = false;
                }
            }
        }

        public void updateGroupSize(int size)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(() => labelServerPlayers.Text = "Players: " + size.ToString() + "/2"));
            else
                labelServerPlayers.Text = "Players: " + size.ToString() + "/2";
        }

        public void updateReadyCheckSize(int size)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(() => labelReadyServer1.Text = "Ready: " + size.ToString() + "/2"));
            else
                labelReadyServer1.Text = "Ready: " + size.ToString() + "/2";
        }

        public void joinServerButton1_Click(object sender, EventArgs e)
        {
            ClientOptionsAlgorithm ServerGroup = new JoinServerGroup();
            ServerGroup.action(ref _signalRConnection, ref _hubProxy, instance);
        }

        public void leaveServerButton1_Click(object sender, EventArgs e)
        {
            ClientOptionsAlgorithm LeaveServerGroup = new LeaveServerGroup();
            LeaveServerGroup.action(ref _signalRConnection, ref _hubProxy, instance);
        }

        public void readyServerButton1_Click(object sender, EventArgs e)
        {
            ClientOptionsAlgorithm Ready = new ReadyForGame();
            Ready.action(ref _signalRConnection, ref _hubProxy, instance);
        }

        public void btnPlay_Click(object sender, EventArgs e)
        {
            ClientOptionsAlgorithm Start = new PlayGame();
            Start.action(ref _signalRConnection, ref _hubProxy, instance);
            
        }

        public string GetName()
        {
            return txtUserName.Text;
        }
        public string GetGroup()
        {
            return grpServer1.Text;
        }
        public Button getbtnConnect()
        {
            return btnConnect;
        }
        public Button getbtnDisconnect()
        {
            return btnDisconnect;
        }
        public Button getleaveServerButton1()
        {
            return leaveServerButton1;
        }
        public Button getreadyServerButton1()
        {
            return readyServerButton1;
        }
        public Button getbtnPlay()
        {
            return btnPlay;
        }
        public Button getjoinServerButton1()
        {
            return joinServerButton1;
        }
        public TextBox gettxtUrl()
        {
            return txtUrl;
        }
        public TextBox gettxtGroupName()
        {
            return txtGroupName;
        }
        public TextBox gettxtMessage()
        {
            return txtMessage;
        }
        public TextBox gettxtUserName()
        {
            return txtUserName;
        }
        public Label getlabelServerPlayers()
        {
            return labelServerPlayers;
        }
        public Label getlabelReadyServer1()
        {
            return labelReadyServer1;
        }
        public GroupBox getgrpMessaging()
        {
            return grpMessaging;
        }
        public GroupBox getgrpMembership()
        {
            return grpMembership;
        }
        public GroupBox getgrpServer1()
        {
            return grpServer1;
        }
    }
}