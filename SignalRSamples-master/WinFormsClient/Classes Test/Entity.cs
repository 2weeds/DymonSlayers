namespace SgClient1.Classes_Test
{
    public abstract class Entity : GameClass
    {
        public int Health { get; set; }
        public Weapon Weapon { get; set; }
        public object Personality { get; set; }

        public abstract void TakeDamage(int damage);
    }
}
