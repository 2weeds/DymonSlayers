using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SgClient1.Classes_Test;

namespace SgClient1.Memento
{
    internal class CareTaker
    {
        List<PlayerClass.Memento> mementos = new List<PlayerClass.Memento>();
        PlayerClass originator = null;
        public CareTaker(PlayerClass originator)
        {
            this.originator = originator;
        }
        public void Backup()
        {
            mementos.Add(originator.MakeSnapshot());
        }
        public void Undo()
        {
            if (this.mementos.Count == 0)
            {
                return;
            }

            var memento = this.mementos.Last();
            this.mementos.Remove(memento);
            try
            {
                this.originator.Restore(memento);
            }
            catch (Exception)
            {
                this.Undo();
            }

        }
    }
}
