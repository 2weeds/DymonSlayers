using InterpreterConsole.Interpreter;
using Microsoft.AspNet.SignalR.Client;
using SgClient1.Strategy;
using System;
using System.Threading.Tasks;
using WinFormsClient;

namespace InterpreterConsole
{
    class Program
    {
        FrmClient form = new FrmClient();
        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine("Enter command: connect, join, ready, not ready, leave.");
            Console.WriteLine("To quit, enter stop.");
            string command = "";
            do
            {
                command = Console.ReadLine();
                //pickCommand(command);
            }
            while (command != "stop");
        }

       /* private static void pickCommand(string command)
        {
            CommandExpression cmd;
            var instanc = new FrmClient();
            switch (command)
            {
                case "connect":
                    instanc.connectAsync();
                    //Console.WriteLine("Connected to server");
                    break;
                case "join":
                    cmd = new SingleCommand(new JoinServerGroup(instanc._signalRConnection, instanc._hubProxy, instanc.getInstance()));
                    //Console.WriteLine("Joined server");
                    break;
                case "ready":
                    Console.WriteLine("Ready");
                    break;
                case "not ready":
                    Console.WriteLine("Not ready");
                    break;
                case "leave":
                    Console.WriteLine("Left server");
                    break;
                case "stop":
                    Console.WriteLine("Stopped. Thank you for using our system.");
                    break;
                default:
                    Console.WriteLine("wrong command");
                    break;
            }
        }

        private async Task connectAsync(FrmClient form)
        {
            //Create a connection for the SignalR server
            form._signalRConnection = new HubConnection("http://localhost:8080/signalr");
            form._signalRConnection.StateChanged += this.HubConnection_StateChanged;

            //Get a proxy object that will be used to interact with the specific hub on the server
            //Ther may be many hubs hosted on the server, so provide the type name for the hub
            form._hubProxy = form._signalRConnection.CreateHubProxy("SimpleHub");

            //Reigster to the "AddMessage" callback method of the hub
            //This method is invoked by the hub
            //form._hubProxy.On<string, string>("AddMessage", (name, message) => this.writeToLog($"{name}:{message}"));
            //form._hubProxy.On<int>("updateMembers", (size) => updateGroupSize(size));
            //form._hubProxy.On<int>("updateReady", (size) => updateReadyCheckSize(size));
            //form._hubProxy.On<int>("getReadyPlayers", (size) => triggerGameForm(size));

            try
            {
                //Connect to the server
                await form._signalRConnection.Start();

                //Send user name for this client, so we won't need to send it with every message
                await form._hubProxy.Invoke("SetUserName", "user1");
            }
            catch (Exception ex)
            {
                //writeToLog($"Error:{ex.Message}");
            }
        }*/

        public void HubConnection_StateChanged(StateChange obj)
        {
            /*if (obj.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
                writeToLog("Connected");
            else if (obj.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
                writeToLog("Disconnected");*/
        }

       /* public void writeToLog(string log)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(() => txtLog.AppendText(log + Environment.NewLine)));
            else
                txtLog.AppendText(log + Environment.NewLine);
        }*/

       /* public void updateGroupSize(int size)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(() => labelServerPlayers.Text = "Players: " + size.ToString() + "/2"));
            else
                labelServerPlayers.Text = "Players: " + size.ToString() + "/2";
        }

        public void updateReadyCheckSize(int size)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(() => labelReadyServer1.Text = "Ready: " + size.ToString() + "/2"));
            else
                labelReadyServer1.Text = "Ready: " + size.ToString() + "/2";
        }

        public void triggerGameForm(int size)
        {
            if (this.InvokeRequired)//to prevent multiple threads accessing same form or smth idk
            {
                this.Invoke((MethodInvoker)delegate
                {
                    triggerGameForm(size);
                });
                return;
            }
            if (size == 2)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() => btnPlay.Visible = true));
                    this.BeginInvoke(new Action(() => btnPlay.Enabled = true));
                    this.BeginInvoke(new Action(() => btnNotReady.Visible = true));
                    this.BeginInvoke(new Action(() => btnNotReady.Enabled = true));
                    this.BeginInvoke(new Action(() => readyServerButton1.Visible = false));
                }
                else
                {
                    btnPlay.Visible = true;
                    btnPlay.Enabled = true;
                    readyServerButton1.Visible = false;
                    instance.getbtnNotReady().Enabled = true;
                    instance.getbtnNotReady().Visible = true;
                }
            }
            else
            {
                btnPlay.Enabled = false;
                readyServerButton1.Visible = true;
            }
        }*/

    }
}
