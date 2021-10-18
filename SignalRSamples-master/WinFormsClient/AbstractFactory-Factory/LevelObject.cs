using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1
{
    public abstract class LevelObject
    {
        public int scaling;

        public abstract AbstractFactory getAbstractFactory();
    }
}
