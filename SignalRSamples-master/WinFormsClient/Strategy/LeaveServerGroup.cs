using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsClient;

namespace SgClient1.Strategy
{
    class LeaveServerGroup : ClientOptionsAlgorithm
    {
        public override void action(ref HubConnection _signalRConnection, ref IHubProxy _hubProxy, FrmClient instance)
        {
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
        }
    }
}
