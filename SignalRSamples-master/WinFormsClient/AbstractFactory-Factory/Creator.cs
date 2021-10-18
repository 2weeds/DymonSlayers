using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1
{
    public abstract class Creator
    {
        public abstract LevelObject factoryMethod(int level);
    }
}
