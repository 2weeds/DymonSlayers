using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;

namespace WinFormsServer
{
    public delegate void ClientConnectionEventHandler(string clientId);
    public delegate void ClientNameChangedEventHandler(string clientId, string newName);
    public delegate void ClientGroupEventHandler(string clientId, string groupName);
    public delegate void ClientSpawnEventHandler(string clientId, string groupName, ConcurrentDictionary<string, List<string>> groupList);

    public delegate void MessageReceivedEventHandler(string senderClientId, string message);

    public class SimpleHub : Hub
    {
        static ConcurrentDictionary<string, string> _users = new ConcurrentDictionary<string, string>();
        static ConcurrentDictionary<string, List<string>> _groups = new ConcurrentDictionary<string, List<string>>();

        public static event ClientConnectionEventHandler ClientConnected;
        public static event ClientConnectionEventHandler ClientDisconnected;
        public static event ClientNameChangedEventHandler ClientNameChanged;

        public static event ClientGroupEventHandler ClientJoinedToGroup;
        public static event ClientGroupEventHandler ClientLeftGroup;
        public static event ClientGroupEventHandler ClientReadyCheck;
        public static event ClientGroupEventHandler ResetClientReadyCheck;

        public static event ClientSpawnEventHandler UpdateSpawn;

        public static event MessageReceivedEventHandler MessageReceived;

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
            UpdateSpawn?.Invoke(Context.ConnectionId, groupName, _groups);
        }


        public void SetUserName(string userName)
        {
            _users[Context.ConnectionId] = userName;

            ClientNameChanged?.Invoke(Context.ConnectionId, userName);
        }
        /// <summary>
        /// Adds a player to Dictionary to group as SignalR does not have a way
        /// to controll groups apart form adding or removing members.
        /// Scuffed checks, but meh.
        /// Called on every 'await Groups.Add'
        /// </summary>
        /// <param name="group">Group name</param>
        void AddToGroups(string group)
        {
            if (!_groups.TryRemove(group, out List<string> bff))
            {
                bff = new List<string>();
            }
            if (!bff.Contains(Context.ConnectionId))
            {
                bff.Add(Context.ConnectionId);
            }
            _groups.TryAdd(group, bff);
        }
        public async Task JoinGroup(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
            // adds user to group dictionary
            AddToGroups(groupName);

            ClientJoinedToGroup?.Invoke(Context.ConnectionId, groupName);
        }

        public async Task ReadyCheck(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
            AddToGroups(groupName);
            ClientReadyCheck?.Invoke(Context.ConnectionId, groupName);
        }

        public async Task ResetReadyCheck(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
            AddToGroups(groupName);
            ResetClientReadyCheck?.Invoke(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.Remove(Context.ConnectionId, groupName);
            // Removes user id from dictionary of groupName
            _groups[groupName].Remove(Context.ConnectionId);

            ClientLeftGroup?.Invoke(Context.ConnectionId, groupName);
        }        

        public void Send(string msg)
        {
            Clients.All.addMessage(_users[Context.ConnectionId], msg);

            MessageReceived?.Invoke(Context.ConnectionId, msg);
        }

        #endregion        
    }
}
