using Microsoft.AspNet.SignalR.Client;
using SgClient1;
using SgClient1.Classes_Test;
using System.Windows.Forms;
using WinFormsClient;
using Xunit;

namespace XUnitTestProject1
{
    public class ObserverTest
    {
        //private PlayerClass player;
        //private IObserver observer;
        //private ISubject subject;
        //private NewsReporter reporter;
        //private IHubProxy _hubProxy;
        //[SetUp]
        //public void initParams()
        //{
        //    player = new PlayerClass();
        //    player.Health = 100;

        //    reporter = new NewsReporter();
        //    reporter._IHubProxy = _hubProxy;
        //}

        //[Test]
        //public void IObserverTest()
        //{
        //    player.Attach(reporter);
        //}
        IHubProxy hubProxy;
        [Fact]
        public void testObserver()
        {
            FrmClient cl = new FrmClient();
            FormGame game = new FormGame(cl.getInstance());
            PictureBox p = game.GetPlayer();
            PlayerClass playerInteractions = new PlayerClass();
            playerInteractions.player = p;
            playerInteractions.form = game;
            playerInteractions.Health = 100;
            playerInteractions.Notify();
            NewsReporter reporter = new NewsReporter
            {
                _IHubProxy = hubProxy
            };
            playerInteractions.Attach(reporter);
        }
    }
}

