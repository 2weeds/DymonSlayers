﻿using SgClient1.Classes_Test;

namespace SgClient1.Strategy
{
    class ThirdLevelStrategy : LevelStrategy
    {
        public override void action(PlayerClass player, Zombie zombie)
        {
            player.speed = 10;
            zombie.speed = player.speed / 2;
        }
    }
}
