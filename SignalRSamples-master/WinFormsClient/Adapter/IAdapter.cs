using SgClient1.Classes_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SgClient1.Adapter
{
    interface IAdapter
    {
        void DoDamage(Entity entity, Control x);
    }
}
