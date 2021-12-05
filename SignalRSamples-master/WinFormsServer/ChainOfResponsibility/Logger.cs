using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsServer;

namespace SgTest3.ChainOfResponsibility
{
    public abstract class Logger
    {
        protected Logger nextChainElement;
        public abstract void writeToLog(string log, FrmServer form);
        public abstract void setNextChain(Logger next);
    }
}
