using Microsoft.AspNet.SignalR.Client;
using System.Windows.Forms;


namespace SgClient1.Classes_Test
{
    public abstract class GameClass : PictureBox
    {
        public FormGame form;
        public PictureBox player;
        public PictureBox player1;
        public IHubProxy _hubProxy;
        public string group;
    }
}
