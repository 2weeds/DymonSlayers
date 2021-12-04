using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Iterator
{
    class ZombieNameCollection : IterableCollection
    {
        private string[] ZombiesList = { "zombie1", "zombie2", "zombie3", "zombie4" };

        public Iterator CreateIterator()
        {
            return new ZombieNameIterator(ZombiesList);
        }
    }
}
