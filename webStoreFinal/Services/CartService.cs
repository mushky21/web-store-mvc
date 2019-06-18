using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Models;

namespace webStoreFinal.Services
{
    public class CartService : ICartService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

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

        //get the hashset of products (which contain for the id for each product) from cookies
        public HashSet<int> ProductsInCookies()
        {
            HashSet<int> cartProductsId;
            string productsCookiesJson = _httpContextAccessor.HttpContext.Request.Cookies["UserProducts"];
            if (string.IsNullOrWhiteSpace(productsCookiesJson))
            {
                cartProductsId = new HashSet<int>();
            }
            else
            {
                cartProductsId = JsonConvert.DeserializeObject<HashSet<int>>(productsCookiesJson);
            }
            return cartProductsId;
        }

        //get the hashset of products from cookies
        //remove product and update cookie and return the updated hashset (for controller of shopping cart)
        public HashSet<int> RemoveProductFromCookie(int id)
        {
            HashSet<int> cartProductsId = ProductsInCookies();
            cartProductsId.Remove(id);
            UpdateCartInCookies(cartProductsId);
            return cartProductsId;
        }

        //update the cart in cookies with new hashset
        public void UpdateCartInCookies(HashSet<int> cartProductsId)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("UserProducts",
           JsonConvert.SerializeObject(cartProductsId),
           new CookieOptions { MaxAge = TimeSpan.FromHours(1) });
        }

        //get products from cookie and add the is to hashset
        //update cookie with updated hashset
        public void AddProductToCookie(int id)
        {
            HashSet<int> cartProductsId = ProductsInCookies();
            cartProductsId.Add(id);
            UpdateCartInCookies(cartProductsId);

        }
    }
}
