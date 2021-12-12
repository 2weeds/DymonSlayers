using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SgClient1.Classes_Test;

namespace SgClient1.Visitor
{
    interface IReportToServer
    {

        void ReportEntity(string line);

    }
}
