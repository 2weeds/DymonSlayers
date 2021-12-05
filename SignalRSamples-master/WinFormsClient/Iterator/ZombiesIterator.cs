using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Iterator
{
    class ZombiesIterator : Iterator
    {
        private int currentPosition { get; set; }
        private List<Zombie> ZombiesList{ get; set; }

        public ZombiesIterator(List<Zombie> ZombiesList)
        {
            this.ZombiesList = ZombiesList;
            currentPosition = 0;
        }

        public Object GetNext()
        {
            if (HasMore())
                return ZombiesList[currentPosition++];
            return null;
        }

        public bool HasMore()
        {
            if (currentPosition >= ZombiesList.Count ||
                ZombiesList[currentPosition] == null)
                return false;
            else return true;
        }
    }
}
