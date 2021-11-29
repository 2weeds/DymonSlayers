using SgClient1;
using WinFormsClient;
using Xunit;
using System.Diagnostics;
using System;

namespace XUnitTestProject1
{
    public class FactoryTest
    {
        [Fact]
        public void testFactory()
        {
            Process currentProcess = Process.GetCurrentProcess();
            long usedMemoryBefore = currentProcess.PrivateMemorySize64;
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
            long usedMemoryAfter = currentProcess.PrivateMemorySize64;
            Console.WriteLine("Memory used before is {0}, memory used after is {1}", usedMemoryBefore, usedMemoryAfter);
        }
    }
}
