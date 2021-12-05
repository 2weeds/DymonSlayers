using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsServer;

namespace SgTest3.ChainOfResponsibility
{
    class FormLogger : Logger
    {
        public override void writeToLog(string log, FrmServer form)
        {
            try
            {
                form.txtLog.AppendText(log + Environment.NewLine);
            }
            catch (Exception ex)
            {
                nextChainElement.writeToLog(log, form);
            }
        }

        public override void setNextChain(Logger next)
        {
            nextChainElement = next;
        }
    }
}
