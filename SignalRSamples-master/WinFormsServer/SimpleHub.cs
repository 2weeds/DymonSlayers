using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace WinFormsServer
{
    public delegate void ClientConnectionEventHandler(string clientId);
    public delegate void ClientNameChangedEventHandler(string clientId, string newName);
    public delegate void ClientGroupEventHandler(string clientId, string groupName);

    public delegate void MessageReceivedEventHandler(string senderClientId, string message);

    public delegate void SpawnerEventHandler(string clientId, string groupName, int x, int y);

    public delegate void PlayerEventHandler(string clientId, string groupName, string userName);

    public class SimpleHub : Hub
    {
        static ConcurrentDictionary<string, string> _users = new ConcurrentDictionary<string, string>();

        public static event ClientConnectionEventHandler ClientConnected;
        public static event ClientConnectionEventHandler ClientDisconnected;
        public static event ClientNameChangedEventHandler ClientNameChanged;

        public static event ClientGroupEventHandler ClientJoinedToGroup;
        public static event ClientGroupEventHandler ClientLeftGroup;
        public static event ClientGroupEventHandler ClientLeftReady;
        public static event ClientGroupEventHandler ClientReadyCheck;
        public static event ClientGroupEventHandler ResetClientReadyCheck;

        public static event ClientGroupEventHandler UpdateSpawn;

        public static event MessageReceivedEventHandler MessageReceived;

        public static event SpawnerEventHandler OutOfBullets;
        public static event SpawnerEventHandler OutOfZombies;
        public static event SpawnerEventHandler DropHealthPack;
        public static event SpawnerEventHandler DropFireWall;

        public static event PlayerEventHandler DeadPlayer;

        public static void ClearState()
        {
            _users.Clear();
        }

        //Called when a client is connected
        public override Task OnConnected()
        {
            _users.TryAdd(Context.ConnectionId, Context.ConnectionId);

            ClientConnected?.Invoke(Context.ConnectionId);

            return base.OnConnected();
        }

        //Called when a client is disconnected
        public override Task OnDisconnected(bool stopCalled)
        {
            string userName;
            _users.TryRemove(Context.ConnectionId, out userName);

            ClientDisconnected?.Invoke(Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        #region Client Methods

        public void UpdateSpawns(string groupName)
        {
            UpdateSpawn?.Invoke(Context.ConnectionId, groupName);
        }

        public void UpdateZombies(string groupName, int x, int y)
        {
            OutOfZombies?.Invoke(Context.ConnectionId, groupName, x, y);
        }

        public void SetUserName(string userName)
        {
            _users[Context.ConnectionId] = userName;

            ClientNameChanged?.Invoke(Context.ConnectionId, userName);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);

            ClientJoinedToGroup?.Invoke(Context.ConnectionId, groupName);
        }

        public async Task ReadyCheck(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);

            ClientReadyCheck?.Invoke(Context.ConnectionId, groupName);
        }

        public async Task ResetReadyCheck(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);

            ResetClientReadyCheck?.Invoke(Context.ConnectionId, groupName);
        }

        public async Task LeaveReady(string groupName)
        {
            await Groups.Remove(Context.ConnectionId, groupName);

            ClientLeftReady?.Invoke(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.Remove(Context.ConnectionId, groupName);

            ClientLeftGroup?.Invoke(Context.ConnectionId, groupName);
        }

        public void Send(string msg)
        {
            Clients.All.addMessage(_users[Context.ConnectionId], msg);

            MessageReceived?.Invoke(Context.ConnectionId, msg);
        }

        public void UpdateBullets(string groupName, int x, int y)
        {
            OutOfBullets?.Invoke(Context.ConnectionId, groupName, x, y);
        }

        public void UpdateHealthPacks(string groupName, int x, int y)
        {
            DropHealthPack?.Invoke(Context.ConnectionId, groupName, x, y);
        }

        public void UpdateFireWalls(string groupName, int x, int y)
        {
            DropFireWall?.Invoke(Context.ConnectionId, groupName, x, y);
        }

        public void PlayerDied(string groupName, string userName)
        {
            DeadPlayer?.Invoke(Context.ConnectionId, groupName, userName);
        }

        public void SaveGame()
        {

        }
        #endregion
    }
}
