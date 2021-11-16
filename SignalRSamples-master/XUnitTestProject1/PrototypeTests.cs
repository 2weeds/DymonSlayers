using SgClient1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsClient;
using Xunit;

namespace XUnitTestProject1
{
    public class PrototypeTests
    {

        FormGame CreateGame()
        {
            return new FormGame();
        }

        [Fact]
        public void TestingCopy()
        {
            //ARRANGE
            FormGame instance = CreateGame();

            //ACT
            FormGame deepCopy = instance.copyDeep();
            
            //ASSERT
            Assert.NotEqual(instance.GetHashCode(), deepCopy.GetHashCode());
        }

        [Fact]
        public void TestingShallowCopy()
        {
            //ARRANGE
            FormGame instance = CreateGame();

            //ACT
            FormGame shallowCopy = instance.copyShallow();

            //ASSERT
            Assert.Equal(instance.GetHashCode(), shallowCopy.GetHashCode());
        }
    }
}
