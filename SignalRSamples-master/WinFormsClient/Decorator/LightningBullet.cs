﻿using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Decorator
{
    class LightningBullet : Decorator
    {
        public LightningBullet(PlayerClass component) : base(component)
        {
        }
        public void LightningShoot(string direct)
        {
            shoot(direct, "Lightning");
        }

        private void shoot(string direct, string bulletType)
        {
            base.shoot(direct, bulletType);
        }
    } 
}