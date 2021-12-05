using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WinFormsServer;

namespace SgTest3.ChainOfResponsibility
{
    class TextLogger : Logger
    {
        private readonly string fileName = "gameLogs.txt";
        public override void writeToLog(string log, FrmServer form)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    using (StreamWriter sw = File.AppendText(fileName))
                    {
                        sw.WriteLine(log);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        sw.WriteLine(log);
                    }
                }
            } catch (Exception ex)
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
