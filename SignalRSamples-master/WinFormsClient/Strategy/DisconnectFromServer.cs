using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsClient;

namespace SgClient1.Strategy
{
    class DisconnectFromServer : ClientOptionsAlgorithm
    {
        public override void action(ref HubConnection _signalRConnection, ref IHubProxy _hubProxy, FrmClient instance)
        {
            ClientOptionsAlgorithm LeaveGroup = new LeaveServerGroup();
            LeaveGroup.action(ref _signalRConnection, ref _hubProxy, instance);

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
            }
        }
    }
}
