using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsClient;

namespace SgClient1.Strategy
{
    class JoinServerGroup : ClientOptionsAlgorithm
    {
        public override void action(ref HubConnection _signalRConnection, ref IHubProxy _hubProxy, FrmClient instance)
        {
            _hubProxy.Invoke("JoinGroup", instance.getgrpServer1().Text);
            instance.getreadyServerButton1().Visible = true;
            instance.getjoinServerButton1().Visible = false;
            instance.getreadyServerButton1().Enabled = true;
            instance.getjoinServerButton1().Enabled = false;
            instance.getleaveServerButton1().Visible = true;
            instance.getleaveServerButton1().Enabled = true;
        }
    }
}
