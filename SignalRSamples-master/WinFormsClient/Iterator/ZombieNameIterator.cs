using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Iterator
{
    class ZombieNameIterator : Iterator
    {
        private int currentPosition { get; set; }
        private string[] ZombiesList { get; set; }

        public ZombieNameIterator(string[] ZombiesList)
        {
            this.ZombiesList = ZombiesList;
            currentPosition = 0;
        }

        public Object GetNext()
        {
            return ZombiesList[currentPosition++];
        }

        public bool HasMore()
        {
            if (currentPosition >= ZombiesList.Length ||
                ZombiesList[currentPosition] == null)
                return false;
            else return true;
        }
    }
}
