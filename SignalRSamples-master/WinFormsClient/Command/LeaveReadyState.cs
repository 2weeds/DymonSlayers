﻿using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsClient;

namespace SgClient1.Command
{
    class LeaveReadyState : ICommand
    {
        private HubConnection _signalRConnection { get; set; }
        private IHubProxy _hubProxy { get; set; }
        private FrmClient instance { get; set; }

        public LeaveReadyState(HubConnection _signalRConnection, IHubProxy _hubProxy, FrmClient instance)
        {
            this._signalRConnection = _signalRConnection;
            this._hubProxy = _hubProxy;
            this.instance = instance;
        }

        public override void run()
        {
            _hubProxy.Invoke("LeaveReady", instance.getgrpServer1().Text);
            instance.getlabelReadyServer1().Text = "Ready 0/2";
            instance.getreadyServerButton1().Visible = true;
            instance.getreadyServerButton1().Enabled = true;
            instance.getleaveServerButton1().Visible = true;
            instance.getleaveServerButton1().Enabled = true;
            instance.getbtnPlay().Visible = false;
            instance.getbtnPlay().Enabled = false;
            instance.getbtnNotReady().Enabled = false;
            instance.getbtnNotReady().Visible = false;
        }

        public override void undo()
        {
            _hubProxy.Invoke("ReadyCheck", instance.getgrpServer1().Text);
            instance.getreadyServerButton1().Enabled = false;
            instance.getbtnNotReady().Enabled = true;
            instance.getbtnNotReady().Visible = true;
        }
    }
}
