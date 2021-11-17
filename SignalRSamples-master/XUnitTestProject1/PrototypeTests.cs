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

        [Theory]
        [InlineData("deep")]
        [InlineData("shallow")]
        public void TestingCopy(string type)
        {
            //ARRANGE
            FormGame instance = CreateGame();

            switch (type)
            {
                case "deep":
                    //ACT
                    FormGame deepCopy = instance.copyDeep();
                    //ASSERT
                    Assert.NotEqual(instance.GetHashCode(), deepCopy.GetHashCode());
                    break;
                case "shallow":
                    //ACT
                    FormGame shallowCopy = instance.copyShallow();
                    //ASSERT
                    Assert.Equal(instance.GetHashCode(), shallowCopy.GetHashCode());
                    break;
                default:
                    Assert.Equal(0, 1);
                    break;
            }
        }
    }
}
