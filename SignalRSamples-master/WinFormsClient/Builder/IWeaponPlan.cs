namespace SgClient1.Builder
{
    interface IWeaponPlan
    {
        /// <summary>
        /// Set to 0 for melee, set to 1 for ranged.
        /// </summary>
        /// <param name="range"></param>
        void SetWeaponRange(int range);
        /// <summary>
        /// Does nothing for now, just a name of the weapon.
        /// </summary>
        /// <param name="type"></param>
        void SetWeaponType(string type);
        /// <summary>
        /// Set weapon damage.
        /// </summary>
        /// <param name="damage"></param>
        void SetWeaponDamage(int damage);
    }
}
