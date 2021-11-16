using SgClient1.Command;
using WinFormsClient;
using SgClient1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsClient;
using Xunit;
using SgClient1.Strategy;

namespace XUnitTestProject1
{
    public class CommandTests
    {
        FrmClient getclient()
        {
            return new FrmClient().getInstance();
        }

        [Theory]
        [InlineData("join")]
        [InlineData("ready")]
        [InlineData("leaveGroup")]
        [InlineData("notReady")]
        public void TestCommand(string command)
        {
            //ARRANGE
            FrmClient cl = getclient();
            var players = (cl.tekstas);

            //ACT
            cl.pickCommand(command);

            //ASSERT
            var players2 = (cl.tekstas);
            Assert.NotEqual(players, players2);
        } 
    }
}
