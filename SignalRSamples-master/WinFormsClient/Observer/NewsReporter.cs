using Microsoft.AspNet.SignalR.Client;
using SgClient1.Observer;
using System;

namespace SgClient1.Classes_Test
{
    public class NewsReporter : IObserver
    {
        public IHubProxy _IHubProxy;

        public NewsReporter()
        {
        }
        public void Update(ISubject subject)
        {
            if (subject is PlayerClass player)
            {
                _IHubProxy.Invoke("Send", String.Format("reporting health {0}", player.Health));
            }
        }
    }
}
