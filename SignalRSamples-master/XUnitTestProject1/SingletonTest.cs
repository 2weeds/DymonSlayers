using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsClient;
using Xunit;

namespace XUnitTestProject1
{
    public class SingletonTest
    {
        FrmClient CreateForm()
        {
            return new FrmClient().getInstance();
        }
        [Fact]
        public void TestSingleton()
        {
            FrmClient instance = CreateForm();
            string a = instance.gettxtUserName().Text;
            instance.settxtUserName("b");
            string b = instance.gettxtUserName().Text;
            Assert.True(b == "b");
        }
    }
}
