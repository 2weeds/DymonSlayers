using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Proxy
{
    internal abstract class ProxyHub : IHubProxy
    {
        protected PlayerClass player;
        public void AddPlayer(PlayerClass player)
        {
            this.player = player;
        }
        protected ProxyHub(IHubProxy proxy)
        {
            iHubProxy = proxy;
        }
        protected IHubProxy iHubProxy;
        public abstract Task Invoke(string method, params object[] args);

        //Methods kept unchanged..
        #region IHubProxy methods
        public JToken this[string name] { get => iHubProxy[name]; set => iHubProxy[name]=value; }

        public JsonSerializer JsonSerializer => iHubProxy.JsonSerializer;

        public Task<T> Invoke<T>(string method, params object[] args)
        {
            return iHubProxy.Invoke<T>(method, args);
        }

        public Task Invoke<T>(string method, Action<T> onProgress, params object[] args)
        {
            return iHubProxy.Invoke<T>(method, onProgress, args);
        }

        public Task<TResult> Invoke<TResult, TProgress>(string method, Action<TProgress> onProgress, params object[] args)
        {
            return iHubProxy.Invoke<TResult, TProgress>(method, onProgress, args);
        }

        public Subscription Subscribe(string eventName)
        {
            return iHubProxy.Subscribe(eventName);
        }
#endregion
    }
}
