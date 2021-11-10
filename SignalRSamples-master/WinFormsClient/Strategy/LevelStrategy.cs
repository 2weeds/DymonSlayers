using SgClient1.Classes_Test;

namespace SgClient1.Strategy
{
    abstract class LevelStrategy
    {
        public abstract void action(PlayerClass player, Zombie zombie);
    }
}
