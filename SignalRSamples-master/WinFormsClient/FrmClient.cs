using Microsoft.AspNet.SignalR.Client;
using SgClient1;
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
        HubConnection _signalRConnection;
        //Proxy object for a hub hosted on the SignalR server
        IHubProxy _hubProxy;

        public FrmClient()
        {
            InitializeComponent();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            await connectAsync();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            //Close the server connection if exists
            if (_signalRConnection != null)
            {
                _signalRConnection.Stop();
                _signalRConnection.Dispose();
                _signalRConnection = null;

                btnConnect.Enabled = true;
                txtUrl.Enabled = true;
                txtUserName.Enabled = true;
                btnDisconnect.Enabled = false;
                grpMessaging.Enabled = false;
                grpMembership.Enabled = false;

            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //Call the "Send" method on the hub (on the server) with the given parameters
            _hubProxy.Invoke("Send", txtMessage.Text);
        }

        private void btnJoinGroup_Click(object sender, EventArgs e)
        {
            //Call the "JoinGroup" method on the hub (on the server)
            _hubProxy.Invoke("JoinGroup", txtGroupName.Text);
        }

        private void btnLeaveGroup_Click(object sender, EventArgs e)
        {
            //Call the "LeaveGroup" method on the hub (on the server)
            _hubProxy.Invoke("LeaveGroup", txtGroupName.Text);
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

        private void HubConnection_StateChanged(StateChange obj)
        {
            if (obj.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
                writeToLog("Connected");
            else if (obj.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
                writeToLog("Disconnected");
        }

        private void writeToLog(string log)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(() => txtLog.AppendText(log + Environment.NewLine)));
            else
                txtLog.AppendText(log + Environment.NewLine);
        }

        private void triggerGameForm(int size)
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

        private void updateGroupSize(int size)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(() => labelServerPlayers.Text = "Players: " + size.ToString() + "/2"));
            else
                labelServerPlayers.Text = "Players: " + size.ToString() + "/2";
        }

        private void updateReadyCheckSize(int size)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(() => labelReadyServer1.Text = "Ready: " + size.ToString() + "/2"));
            else
                labelReadyServer1.Text = "Ready: " + size.ToString() + "/2";
        }

        private void FrmClient_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void joinServerButton1_Click(object sender, EventArgs e)
        {
            _hubProxy.Invoke("JoinGroup", grpServer1.Text);
            readyServerButton1.Visible = true;
            joinServerButton1.Visible = false;
            readyServerButton1.Enabled = true;
            joinServerButton1.Enabled = false;
        }

        private void leaveServerButton1_Click(object sender, EventArgs e)
        {
            _hubProxy.Invoke("LeaveGroup", grpServer1.Text);
            labelServerPlayers.Text = "Players 0/2";
            labelReadyServer1.Text = "Ready 0/2";
            joinServerButton1.Visible = true;
            joinServerButton1.Enabled = true;
            readyServerButton1.Visible = false;
            readyServerButton1.Enabled = false;
            btnPlay.Visible = false;
            btnPlay.Enabled = false;

        }

        private void readyServerButton1_Click(object sender, EventArgs e)
        {
            _hubProxy.Invoke("ReadyCheck", grpServer1.Text);
            readyServerButton1.Enabled = false;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            _hubProxy.Invoke("ResetReadyCheck", grpServer1.Text);
            readyServerButton1.Visible = true;
            readyServerButton1.Enabled = true;
            btnPlay.Visible = false;
            btnPlay.Enabled = false;
            //FormGame gamefrm = new FormGame();
            //gamefrm.Show();
            FrmTest test = new FrmTest(_signalRConnection, _hubProxy, grpServer1.Text);
            test.Show();
        }
    }
}