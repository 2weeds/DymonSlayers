namespace SgClient1
{
    public abstract class Unit
    {
        public int value;

        public PickupFactory p = new PickupFactory();

        public abstract void spawnUnit(FormGame form, int x, int y);

    }
}
