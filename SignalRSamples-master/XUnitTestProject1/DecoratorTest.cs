using SgClient1;
using SgClient1.Classes_Test;
using SgClient1.Decorator;
using System.Windows.Forms;
using WinFormsClient;
using Xunit;

namespace XUnitTestProject1
{
    public class DecoratorTest
    {
        [Fact]
        public void testDecorator()
        {
            FrmClient cl = new FrmClient();
            FormGame game = new FormGame(cl.getInstance());
            PictureBox p = game.GetPlayer();
            PlayerClass playerInteractions = new PlayerClass();
            playerInteractions.player = p;
            playerInteractions.form = game;
            IceBullet ice = new IceBullet(playerInteractions); ;
            FireBullet fire = new FireBullet(playerInteractions);
            LightningBullet lightning = new LightningBullet(playerInteractions);
            fire.FireShoot("left");
            ice.IceShoot("right");
            lightning.LightningShoot("down");
        }
    }
}
