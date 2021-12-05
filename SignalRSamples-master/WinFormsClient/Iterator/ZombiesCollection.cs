using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Iterator
{
    class ZombiesCollection
    {
        private List<Zombie> ZombiesList = new List<Zombie>();

        public Iterator CreateIterator()
        {
            return new ZombiesIterator(ZombiesList);
        }

        public void AddItem(Zombie zombie)
        {
            ZombiesList.Add(zombie);
        }

        public void RemoveItem(Zombie zombie)
        {
            ZombiesList.Remove(zombie);
        }
    }
}
