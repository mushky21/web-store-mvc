using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webStoreFinal.Services
{
    public interface IHighlightActive
    {
        string classForNavLink(bool isActive);
    }
}
