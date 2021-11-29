namespace FlyweightTest
{
    public class L3 : LevelObject
    {
        public override AbstractFactory getAbstractFactory()
        {
            return new L3Factory();
        }
    }
}
