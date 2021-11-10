using SgClient1.Classes_Test;

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
