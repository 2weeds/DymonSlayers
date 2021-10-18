using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsClient;

namespace SgClient1.Strategy
{
    public abstract class ClientOptionsAlgorithm
    {
        public abstract void action(ref HubConnection _signalRConnection, ref IHubProxy _hubProxy, FrmClient instance);
    }
}
