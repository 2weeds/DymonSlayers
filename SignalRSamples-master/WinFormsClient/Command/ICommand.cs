using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Command
{
    public abstract class ICommand
    {
        public abstract void run();
        public abstract void undo();
    }
}
