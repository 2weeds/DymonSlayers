using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Observer
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }
    
}
