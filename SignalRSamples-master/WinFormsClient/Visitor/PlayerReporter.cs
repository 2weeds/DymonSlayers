using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using SgClient1.Classes_Test;

namespace SgClient1.Visitor
{
    class PlayerReporter : IReportToServer
    {
        public IHubProxy _IHubProxy;

        public PlayerReporter()
        {
        }
        public void ReportEntity(string line)
        {
            _IHubProxy.Invoke("Send", line);
        }

    }
}
