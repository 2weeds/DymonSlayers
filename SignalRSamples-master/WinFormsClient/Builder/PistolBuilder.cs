﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SgClient1.Classes_Test;
using SgClient1.Observer;

namespace SgClient1.Builder
{
    class PistolBuilder : IWeaponBuilder
    {
        private Weapon pistol;
        public PistolBuilder()
        {
            pistol = new Weapon();
        }
        public void BuildWeaponDamage()
        {
            pistol.SetWeaponDamage(1);
        }

        public void BuildWeaponRange()
        {
            pistol.SetWeaponRange(1);
        }

        public void BuildWeaponType()
        {
            pistol.SetWeaponType("Pistol");
        }

        public Weapon GetWeapon()
        {
            return pistol;
        }
    }
}
