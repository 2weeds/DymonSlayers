using Microsoft.AspNet.SignalR.Client;
using SgClient1;
using SgClient1.Command;
using SgClient1.Strategy;
using System;
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
        private CommandController CommandRunner;
        public FrmClient()
        {
            instance = this;
            InitializeComponent();
            CommandRunner = new CommandController();
        }
        public FrmClient getInstance() { return instance; }

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

                instance.getbtnConnect().Enabled = true;
                instance.gettxtUrl().Enabled = true;
                instance.gettxtUserName().Enabled = true;
                instance.getbtnDisconnect().Enabled = false;
                instance.getgrpMessaging().Enabled = false;
                instance.getgrpMembership().Enabled = false;
                instance.getbtnNotReady().Enabled = false;
                instance.getbtnNotReady().Visible = false;
                instance.getreadyServerButton1().Visible = false;
                instance.getreadyServerButton1().Enabled = false;
                instance.getleaveServerButton1().Visible = false;
                instance.getleaveServerButton1().Enabled = false;
                instance.getbtnUndo().Visible = false;
                instance.getbtnUndo().Enabled = false;
                instance.getbtnPlay().Visible = false;
                instance.getbtnPlay().Enabled = false;
                instance.getjoinServerButton1().Enabled = false;
                instance.getjoinServerButton1().Visible = false;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //Call the "Send" method on the hub (on the server) with the given parameters
            _hubProxy.Invoke("Send", instance.gettxtMessage().Text);
        }

        private void btnJoinGroup_Click(object sender, EventArgs e)
        {
            //Call the "JoinGroup" method on the hub (on the server)
            _hubProxy.Invoke("JoinGroup", instance.gettxtGroupName().Text);
        }

        private void btnLeaveGroup_Click(object sender, EventArgs e)
        {
            //Call the "LeaveGroup" method on the hub (on the server)
            _hubProxy.Invoke("LeaveGroup", instance.gettxtGroupName().Text);
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
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() => btnPlay.Visible = true));
                    this.BeginInvoke(new Action(() => btnPlay.Enabled = true));
                    this.BeginInvoke(new Action(() => btnNotReady.Visible = true));
                    this.BeginInvoke(new Action(() => btnNotReady.Enabled = true));
                    this.BeginInvoke(new Action(() => readyServerButton1.Visible = false));
                }
                else
                {
                    btnPlay.Visible = true;
                    btnPlay.Enabled = true;
                    readyServerButton1.Visible = false;
                    instance.getbtnNotReady().Enabled = true;
                    instance.getbtnNotReady().Visible = true;
                }
            }
            else
            {
                btnPlay.Enabled = false;
                readyServerButton1.Visible = true;
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

        private void joinServerButton1_Click(object sender, EventArgs e)
        {
            pickCommand("join");
        }

        private void leaveServerButton1_Click(object sender, EventArgs e)
        {
            pickCommand("leaveGroup");
        }

        private void readyServerButton1_Click(object sender, EventArgs e)
        {
            pickCommand("ready");
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (CommandRunner.undo() == "disable")
            {
                btnUndo.Visible = btnUndo.Enabled = false;
            }
        }

        private void btnNotReady_Click(object sender, EventArgs e)
        {
            pickCommand("notReady");
        }

        public void btnPlay_Click(object sender, EventArgs e)
        {
            _hubProxy.Invoke("ResetReadyCheck", instance.getgrpServer1().Text);
            instance.getreadyServerButton1().Visible = true;
            instance.getreadyServerButton1().Enabled = true;
            instance.getbtnPlay().Visible = false;
            instance.getbtnPlay().Enabled = false;
            //FormGame gamefrm = new FormGame(_signalRConnection, _hubProxy, grpServer1.Text, txtUserName.Text);
            FormGame gamefrm = new FormGame(instance);
            gamefrm.Show();
        }

        private void pickCommand(string command)
        {
            ICommand runnableCommand = null;

            if (command == "join")
            {
                runnableCommand = new JoinServerGroup(_signalRConnection, _hubProxy, instance);
            }
            else if (command == "ready")
            {
                runnableCommand = new ReadyForGame(_signalRConnection, _hubProxy, instance);
            }
            else if (command == "leaveGroup")
            {
                runnableCommand = new LeaveServerGroup(_signalRConnection, _hubProxy, instance);
            }
            else if (command == "notReady")
            {
                runnableCommand = new LeaveReadyState(_signalRConnection, _hubProxy, instance);
            }

            if (runnableCommand != null)
            {
                CommandRunner.run(runnableCommand);
            }
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
        public Button getbtnUndo()
        {
            return btnUndo;
        }
        public Button getbtnNotReady()
        {
            return btnNotReady;
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
        public void settxtUserName(string uname)
        {
            txtUserName.Text = uname;
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