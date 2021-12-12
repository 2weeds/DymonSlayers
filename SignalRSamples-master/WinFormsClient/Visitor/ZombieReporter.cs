using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using SgClient1.Classes_Test;

namespace SgClient1.Visitor
{
    class ZombieReporter : IReportToServer
    {
        public IHubProxy _IHubProxy;

        public ZombieReporter()
        {
        }
        public void ReportEntity(PlayerClass player)
        {
            throw new NotImplementedException();
        }

        public void ReportEntity(string line)
        {
            _IHubProxy.Invoke("Send", line);
        }
    }
}
