using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SgClient1.Classes_Test;

namespace SgClient1.Builder
{
    class KillerHandsBuilder : IWeaponBuilder
    {
        Weapon killerHands;
        public KillerHandsBuilder()
        {
            killerHands = new Weapon();
        }
        public void BuildWeaponDamage()
        {
            killerHands.SetWeaponDamage(1);
        }

        public void BuildWeaponRange()
        {
            killerHands.SetWeaponRange(0);
        }

        public void BuildWeaponType()
        {
            killerHands.SetWeaponType("KillerHands");
        }

        public Weapon GetWeapon()
        {
            return killerHands;
        }
    }
}
