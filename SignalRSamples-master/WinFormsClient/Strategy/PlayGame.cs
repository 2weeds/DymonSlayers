using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsClient;

namespace SgClient1.Strategy
{
    class PlayGame : ClientOptionsAlgorithm
    {
        public override void action(ref HubConnection _signalRConnection, ref IHubProxy _hubProxy, FrmClient instance)
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
    }
}
