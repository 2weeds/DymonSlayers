using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Windows.Forms;
using NUnit.Framework;
using WinFormsClient;
using WinFormsServer;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        public delegate void ClientConnectionEventHandler(string clientId);
        public delegate void ClientNameChangedEventHandler(string clientId, string newName);
        public delegate void ClientGroupEventHandler(string clientId, string groupName);

        public delegate void MessageReceivedEventHandler(string senderClientId, string message);

        public delegate void SpawnerEventHandler(string clientId, string groupName, int x, int y);

        public delegate void PlayerEventHandler(string clientId, string groupName, string userName);
        [Fact]
        public void testFacade()
        {
            //IDisposable _signalR;
            //BindingList<ClientItem> _clients = new BindingList<ClientItem>();
            //ClientItem client1 = new ClientItem();
            //client1.Id = "1";
            //client1.Name = "user";
            //_clients.AddNew();
            IDisposable signalR;
            var server = new FrmServer();
            var hub = new SimpleHub();
            var _clients = new BindingList<ClientItem>();
            var clientId = "user";
            server.SimpleHub_ClientConnected("user");
            server.SimpleHub_ClientDisconnected("user");
            ClientItem client = new ClientItem();
            client.Id = "user";
            client.Name = "user";
            string clientProps = client.Id + client.Name;

            ////server.SimpleHub_ClientConnected("user");
            ////server.SimpleHub_ClientDisconnected("user");
        }
    //[Fact]
    //public void PassingTest()
    //{
    //    Assert.Equal(4, Add(2, 2));
    //}

    //[Fact]
    //public void FailingTest()
    //{
    //    Assert.Equal(5, Add(2, 2));
    //}

    //int Add(int x, int y)
    //{
    //    return x + y;
    //}
    //[Theory]
    //[InlineData(3)]
    //[InlineData(5)]
    //[InlineData(6)]
    //public void MyFirstTheory(int value)
    //{
    //    Assert.True(IsOdd(value));
    //}

    //bool IsOdd(int value)
    //{
    //    return value % 2 == 1;
    //}

}
}
