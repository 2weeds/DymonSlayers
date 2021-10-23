﻿using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Builder
{
    class WeaponDirector
    {
        private IWeaponBuilder weaponBuilder;
        public WeaponDirector(IWeaponBuilder builder)
        {
            this.weaponBuilder = builder;
        }
        /// <summary>
        /// Retrieve built weapon.
        /// </summary>
        /// <returns></returns>
        public Weapon GetWeapon()
        {
            return this.weaponBuilder.GetWeapon();
        }
        /// <summary>
        /// Creates a weapon from given builder.
        /// </summary>
        public WeaponDirector MakeWeapon()
        {
            this.weaponBuilder.BuildWeaponDamage();
            this.weaponBuilder.BuildWeaponRange();
            this.weaponBuilder.BuildWeaponType();
            return this;
        }
    }
}
