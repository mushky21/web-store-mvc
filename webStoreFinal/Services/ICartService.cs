using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Models;

namespace webStoreFinal.Services
{
    public interface ICartService
    {
        double VisitorCartSum(List<Product> products);
        double MemberCartSum(List<Product> products);

        List<Product> ShowCart();
        void UpdateCartInCookies(HashSet<int> cartProductsId);
        HashSet<int> ProductsInCookies();
        HashSet<int> RemoveProduct(int id);
        void AddProduct(int id);
        void CompletePurchase();

    }
}
