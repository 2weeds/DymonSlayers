using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SgClient1.Builder;

namespace XUnitTestProject1
{
    public class BuilderTest
    {
        [Theory]
        [InlineData("hands")]
        [InlineData("pistol")]
        public void BuildWeaponTest(string a)
        {
            if (a == "hands")
            {
                Weapon hands = new WeaponDirector(new KillerHandsBuilder()).MakeWeapon().GetWeapon();
                Assert.True(hands.GetWeaponDamage() == 1 && hands.GetWeaponRange() == 0 && hands.GetWeaponType() == "KillerHands");
                return;
            }
            if (a == "pistol")
            {
                Weapon pistol = new WeaponDirector(new PistolBuilder()).MakeWeapon().GetWeapon();
                Assert.True(pistol.GetWeaponDamage() == 1 && pistol.GetWeaponRange() == 1 && pistol.GetWeaponType() == "Pistol");
                return;
            }
            Assert.True(false);
        }
    }
}
