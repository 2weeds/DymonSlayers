using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SgClient1.Classes_Test
{
    abstract class GameClass : PictureBox
    {
        public FormGame form;
        public PictureBox player;
        public PictureBox player1;
        public IHubProxy _hubProxy;
        public string group;
    }
}
