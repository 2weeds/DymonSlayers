namespace FlyweightTest
{
    public class L1 : LevelObject
    {
        public override AbstractFactory getAbstractFactory()
        {
            return new L1Factory();
        }
    }
}
