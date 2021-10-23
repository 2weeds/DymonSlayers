using SgClient1.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Classes_Test
{
    class Weapon : IWeaponPlan
    {
        private int Range;
        private string Type;
        private int Damage;
        /// <summary>
        /// Set weapon damage.
        /// </summary>
        /// <param name="damage"></param>
        public void SetWeaponDamage(int damage)
        {
            this.Damage = damage;
        }
        public int GetWeaponDamage()
        {
            return Damage;
        }
        /// <summary>
        /// Set to 0 for melee, set to 1 for ranged.
        /// </summary>
        /// <param name="range"></param>
        public void SetWeaponRange(int range)
        {
            this.Range = range;
        }
        public int GetWeaponRange()
        {
            return Range;
        }
        /// <summary>
        /// Does nothing for now, just a name of the weapon.
        /// </summary>
        /// <param name="type"></param>
        public void SetWeaponType(string type)
        {
            this.Type = type;
        }
        public string GetWeaponType()
        {
            return Type;
        }
    }
}
