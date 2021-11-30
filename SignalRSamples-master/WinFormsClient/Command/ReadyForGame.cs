using Microsoft.AspNet.SignalR.Client;
using SgClient1.Command;
using WinFormsClient;

namespace SgClient1.Strategy
{
    class ReadyForGame : ICommand
    {
        private HubConnection _signalRConnection { get; set; }
        private IHubProxy _hubProxy { get; set; }
        private FrmClient instance { get; set; }

        public ReadyForGame(HubConnection _signalRConnection, IHubProxy _hubProxy, FrmClient instance)
        {
            this._signalRConnection = _signalRConnection;
            this._hubProxy = _hubProxy;
            this.instance = instance;
        }

        public void run()
        {
            if(_hubProxy != null)
            {
                _hubProxy.Invoke("ReadyCheck", instance.getgrpServer1().Text);
            }
            instance.getreadyServerButton1().Enabled = false;
            instance.getbtnNotReady().Enabled = true;
            instance.getbtnNotReady().Visible = true;
            instance.tekstas = "ready";
        }

        public void undo()
        {
            if (_hubProxy != null)
                _hubProxy.Invoke("LeaveReady", instance.getgrpServer1().Text);
            instance.getlabelReadyServer1().Text = "Ready 0/2";
            instance.getreadyServerButton1().Visible = true;
            instance.getreadyServerButton1().Enabled = true;
            instance.getleaveServerButton1().Visible = true;
            instance.getleaveServerButton1().Enabled = true;
            instance.getbtnPlay().Visible = false;
            instance.getbtnPlay().Enabled = false;
            instance.getbtnNotReady().Enabled = false;
            instance.getbtnNotReady().Visible = false;
            instance.tekstas = "leaveready";
        }
    }
}
