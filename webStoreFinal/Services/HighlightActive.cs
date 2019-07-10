using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webStoreFinal.Services
{
    public class HighlightActive : IHighlightActive
    {
        public string classForNavLink(bool isActive)
        {
            return isActive ? "bg-info text-white nav-link" : "nav-link";
        }
    }
}
