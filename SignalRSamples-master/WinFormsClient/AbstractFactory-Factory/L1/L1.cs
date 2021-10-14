using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1
{
    public class L1 : LevelObject
    {
        public override AbstractFactory getAbstractFactory()
        {
            return new L1Factory();
        }
    }
}
