using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Decorator
{
    public abstract class Decorator
    {
        protected PlayerClass wrappee;
        public Decorator (PlayerClass component)
        {
            wrappee = component;
        }

        public void shoot(string direct, string bulletType)
        {
            wrappee.shoot(direct, bulletType);
        }
    }
}
