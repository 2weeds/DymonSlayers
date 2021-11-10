using SgClient1;
using WinFormsClient;
using Xunit;

namespace XUnitTestProject1
{
    public class FactoryTest
    {
        [Fact]
        public void testFactory()
        {
            FrmClient cl = new FrmClient();
            FormGame game = new FormGame(cl.getInstance());
            Creator lvCreator = new LevelCreator();
            for (int i = 1; i <= 3; i++)
            {
                LevelObject level = lvCreator.factoryMethod(i);
                AbstractFactory objectFactory = level.getAbstractFactory();
                Unit firewall = objectFactory.createFireWall();
                firewall.spawnUnit(game, 100, 100);
                Unit gunBullets = objectFactory.createGunBullets();
                gunBullets.spawnUnit(game, 200, 200);
                Unit healKit = objectFactory.createHealKit();
                healKit.spawnUnit(game, 300, 300);
            }
        }
    }
}
