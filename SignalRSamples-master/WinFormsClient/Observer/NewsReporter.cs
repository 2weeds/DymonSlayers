using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Client;
using SgClient1.Observer;

namespace SgClient1.Classes_Test
{
    class NewsReporter : IObserver
    {
        public IHubProxy _IHubProxy;

        public NewsReporter()
        {
        }
        public void Update(ISubject subject)
        {
            if (subject is PlayerClass player)
            {
                _IHubProxy.Invoke("Send", String.Format("reporting health {0}",player.Health));
            }
        }
    }
}
