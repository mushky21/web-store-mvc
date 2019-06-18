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

        void UpdateCartInCookies(HashSet<int> cartProductsId);
        HashSet<int> ProductsInCookies();
        HashSet<int> RemoveProductFromCookie(int id);
        void AddProductToCookie(int id);

    }
}
