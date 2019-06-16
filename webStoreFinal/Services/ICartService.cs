using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webStoreFinal.Services
{
    public interface ICartService
    {
        double VisitorCartSum(List<double> prices);
        double MemberCartSum(List<double> prices);
    }
}
