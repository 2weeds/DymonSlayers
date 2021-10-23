using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Builder
{
    interface IWeaponBuilder
    {
        void BuildWeaponRange();
        void BuildWeaponType();
        void BuildWeaponDamage();
        Weapon GetWeapon();
    }
}
