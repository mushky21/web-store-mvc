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
        private IProductRepository _productRepository;

        public CartService(IHttpContextAccessor httpContextAccessor, IProductRepository productRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
        }

        public List<Product> ShowCart()
        {
            HashSet<int> cartProductsId = ProductsInCookies();
            var products = from product in _productRepository.Products() //ברפוזיטורי  מחזירים רשימת אוביקטים מסוג מוצר
                           where cartProductsId.Contains(product.ProductKey) // מחפשים את המוצרים שנמצאים בפרמטר שקיבלתי למתודה -משמע מה שנמצא לי בעגלה
                           select product;
            return products.ToList();
        }

        public double MemberCartSum(List<Product> products)
        {
            return VisitorCartSum(products) * ConstDefinitions.MembersDiscount;
        }

        public double VisitorCartSum(List<Product> products)
        {
            return products.Sum(product => product.Price); ;
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
        //remove product and update cookie,change state and return the updated hashset 
        public HashSet<int> RemoveProduct(int id)
        {
            HashSet<int> cartProductsId = ProductsInCookies();
            cartProductsId.Remove(id);
            UpdateCartInCookies(cartProductsId);
            //update state
            _productRepository.UpdateProductState(id, State.Available);
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
        public void AddProduct(int id)
        {
            HashSet<int> cartProductsId = ProductsInCookies();
            cartProductsId.Add(id);
            UpdateCartInCookies(cartProductsId);

            _productRepository.UpdateProductState(id, State.InCart);
        }

        public void CompletePurchase()
        {
           //get from cookies
            HashSet<int> cartProductsId=ProductsInCookies();

           //update all products by state of buyed
           foreach(var productId in cartProductsId)
            {
                _productRepository.UpdateProductBuyer()
            }

           //ovveride cart in empty string or delete?

        }
    }
}
