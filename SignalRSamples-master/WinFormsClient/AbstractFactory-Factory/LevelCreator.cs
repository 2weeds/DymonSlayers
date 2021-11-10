namespace SgClient1
{
    public class LevelCreator : Creator
    {
        public override LevelObject factoryMethod(int level)
        {
            if (level == 1)
            {
                return new L1();
            }
            else if (level == 2)
            {
                return new L2();
            }
            else
            {
                return new L3();
            }
        }
    }
}
