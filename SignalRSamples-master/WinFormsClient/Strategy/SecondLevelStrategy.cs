using SgClient1.Classes_Test;

namespace SgClient1.Strategy
{
    class SecondLevelStrategy : LevelStrategy
    {
        public override void action(PlayerClass player, Zombie zombie)
        {
            player.speed = 7;
            zombie.speed = player.speed / 2;
        }
    }
}
