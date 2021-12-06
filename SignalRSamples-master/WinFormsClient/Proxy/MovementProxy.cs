using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SgClient1.Proxy
{
    internal class MovementProxy : ProxyHub
    {
        public MovementProxy(IHubProxy proxy) : base(proxy)
        {
        }
        //TODO: perhaps do somfing with player. hes not wanted here 
        /// <summary>
        /// Overrided proxy metthod for movement coordinate validation
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override Task Invoke(string method, params object[] args)
        {
            // form size 1085, 601
            //$"m;{direction};{player.Location.X};{player.Location.Y}"
            string command = (string)args[0];
            string[] parts = command.Split(';');
            int x = int.Parse(parts[2]);
            int y = int.Parse(parts[3]);

            if (x < 0) x = 0;
            if (x > 860) x = 860;
            if (y < 60) y = 60;
            if (y > 601) y = 601;
            player.SetNewCoordinates(x, y);
            object[] newArg = { string.Format($"{parts[0]};{parts[1]};{x};{y}") };
            return iHubProxy.Invoke(method, newArg);
        }
    }
}
