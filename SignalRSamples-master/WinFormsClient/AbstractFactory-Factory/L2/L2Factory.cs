﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1
{
    class L2Factory : AbstractFactory
    {
        public override Unit createGunBullets()
        {
            return new AmmoKit2();
        }

        public override Unit createHealKit()
        {
            return new HealKit2();
        }

        public override Unit createFireWall()
        {
            return new FireWall2();
        }
    }
}