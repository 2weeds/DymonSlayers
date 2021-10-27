using SgClient1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsClient;
using Xunit;


namespace XUnitTestProject1
{
    public class MovementTests
    {
        //static PictureBox player1;
        public static IEnumerable<object[]> SetMovement => new List<object[]>
        {
            new object[]{ "unitTester1;left;5;5", new PictureBox() { Location = new Point(0, 0) }, new Point(5, 5) },
            new object[]{ "unitTester2;up;55;1", new PictureBox() { Location = new Point(150, 22) }, new Point(55, 1) }
        };
        [Theory, MemberData(nameof(SetMovement))]
        public void TestGetMovement(string message, PictureBox player1, Point point)
        {
            FrmClient cl = new FrmClient();
            FormGame game = new FormGame(cl.getInstance());
            game.getMovement(message, player1);
            Assert.True(player1.Location == point);
        }
        public static IEnumerable<object[]> MakeMovement => new List<object[]>
        {
            new object[]{ new KeyEventArgs(Keys.A), new object[]{ true, "left", SgClient1.Properties.Resources.left } },
            new object[]{ new KeyEventArgs(Keys.S), new object[]{ true, "down", SgClient1.Properties.Resources.down } }
        };
        [Theory, MemberData(nameof(MakeMovement))]
        public void TestMakeMovement(KeyEventArgs e, object[] res)
        {
            FrmClient cl = new FrmClient();
            FormGame game = new FormGame(cl.getInstance());
            game.godown = false;
            game.goleft = false;
            game.goright = false;
            game.goup = false;
            game.keyisdown(game, e);
            switch (e.KeyCode)
            {
                case (Keys.A):
                    Assert.True(game.goleft == (bool)res[0] && game.facing == (string)res[1]);// TODO: comapre images
                    break;
                case (Keys.S):
                    Assert.True(game.godown == (bool)res[0] && game.facing == (string)res[1]);
                    break;
                default://test fails by default
                    Assert.True(1 == 1);
                    break;
            }
        }
    }
}
