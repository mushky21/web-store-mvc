using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Models;

namespace webStoreFinal.Services
{
    public class CartService : ICartService
    {

        public double MemberCartSum(List<double> prices)
        {
            return VisitorCartSum(prices) * ConstDefinitions.MembersDiscount;
        }

        public double VisitorCartSum(List<double> prices)
        {
            double totalPrice = default(double);
            foreach (var item in prices)
            {
                totalPrice += item;
            }
            return totalPrice;
        }

    }
}
