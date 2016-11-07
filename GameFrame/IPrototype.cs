using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame
{
    public interface IPrototype
    {
        IPrototype Clone();
    }
}
