using SgClient1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class StrategyTests
    {
        FormGame getGame()
        {
            return new FormGame();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TestCommand(int level)
        {
            //ARRANGE
            FormGame cl = getGame();
            var player1 = (cl.playerInteractions.speed);

            //ACT
            cl.setStrategy(level);

            //ASSERT
            var player2 = (cl.playerInteractions.speed);
            Assert.NotEqual(player1, player2);
        }
    }
}
