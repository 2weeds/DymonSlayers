using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1
{
    public abstract class Unit
    {
        public int value;

        public abstract void spawnUnit(FormGame form, int x, int y);
    }
}
