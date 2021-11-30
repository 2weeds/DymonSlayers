using Microsoft.AspNet.SignalR.Client;
using SgClient1.Command;
using WinFormsClient;

namespace SgClient1.Strategy
{
    public class JoinServerGroup : ICommand
    {
        private HubConnection _signalRConnection { get; set; }
        private IHubProxy _hubProxy { get; set; }
        private FrmClient instance { get; set; }

        public JoinServerGroup(HubConnection _signalRConnection, IHubProxy _hubProxy, FrmClient instance)
        {
            this._signalRConnection = _signalRConnection;
            this._hubProxy = _hubProxy;
            this.instance = instance;
        }

        public void run()
        {
            if(_hubProxy != null)
            {
                _hubProxy.Invoke("JoinGroup", instance.getgrpServer1().Text);
            }
            instance.getreadyServerButton1().Visible = true;
            instance.getjoinServerButton1().Visible = false;
            instance.getreadyServerButton1().Enabled = true;
            instance.getjoinServerButton1().Enabled = false;
            instance.getleaveServerButton1().Visible = true;
            instance.getleaveServerButton1().Enabled = true;
           /* instance.getbtnUndo().Enabled = true;
            instance.getbtnUndo().Visible = true;*/
            instance.tekstas = "join";
        }

        public void undo()
        {
            if (_hubProxy != null)
                _hubProxy.Invoke("LeaveGroup", instance.getgrpServer1().Text);

            instance.getlabelServerPlayers().Text = "Players 0/2";
            instance.getlabelReadyServer1().Text = "Ready 0/2";
            instance.getjoinServerButton1().Visible = true;
            instance.getjoinServerButton1().Enabled = true;
            instance.getreadyServerButton1().Visible = false;
            instance.getreadyServerButton1().Enabled = false;
            instance.getleaveServerButton1().Visible = false;
            instance.getleaveServerButton1().Enabled = false;
            instance.getbtnPlay().Visible = false;
            instance.getbtnPlay().Enabled = false;
            instance.getbtnUndo().Enabled = false;
            instance.getbtnUndo().Visible = false;
            instance.getbtnNotReady().Enabled = false;
            instance.getbtnNotReady().Visible = false;
            instance.tekstas = "leavegrp";
        }
    }
}
