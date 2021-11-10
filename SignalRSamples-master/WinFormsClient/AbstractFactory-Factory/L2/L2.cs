namespace SgClient1
{
    public class L2 : LevelObject
    {
        public override AbstractFactory getAbstractFactory()
        {
            return new L2Factory();
        }
    }
}
