using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Models;



namespace webStoreFinal.Services
{
    public class CartService : ICartService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IProductRepository _productRepository;
        private IUserRepository _userRepository;

        public CartService(IHttpContextAccessor httpContextAccessor, IProductRepository productRepository, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public List<Product> ShowCart()
        {
            HashSet<int> cartProductsId = ProductsInCart();
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
        public HashSet<int> ProductsInCart()
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
            HashSet<int> cartProductsId = ProductsInCart();
            cartProductsId.Remove(id);
            UpdateCartInCookies(cartProductsId);
            //update state
            _productRepository.UpdateProductState(id, State.Available);
            return cartProductsId;
        }

        //get products from cookie and add the is to hashset
        //update cookie with updated hashset
        public void AddProduct(int id)
        {
            HashSet<int> cartProductsId = ProductsInCart();
            cartProductsId.Add(id);
            UpdateCartInCookies(cartProductsId);

            _productRepository.UpdateProductState(id, State.InCart);
        }

        //get the cart from cookie, updates state (and buyer id if user is authenticated) and delete from cookies all cart
        public async void CompletePurchase()
        {
            HashSet<int> cartProductsId = ProductsInCart();
            MyUser userAuthenticated = await _userRepository.FindUserAuthenticated();
            if (userAuthenticated != null)
                CompletePurchaseForMember(userAuthenticated.Id, cartProductsId);
            else
            {
                CompletePurchaseForVisitor(cartProductsId);

            }

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserProducts");
        }

        //update status of purchased and buyer id for each product in cart
        private void CompletePurchaseForMember(string buyerId, HashSet<int> cartProductsId)
        {
            foreach (var productId in cartProductsId)
            {
                _productRepository.UpdateProductBuyer(productId, buyerId);
                _productRepository.UpdateProductState(productId, State.Purchased);
            }
        }

        //update status of purchased for each product in cart
        private void CompletePurchaseForVisitor(HashSet<int> cartProductsId)
        {
            foreach (var productId in cartProductsId)
            {
                _productRepository.UpdateProductState(productId, State.Purchased);
            }
        }

        //update the cart in cookies with new hashset
        private void UpdateCartInCookies(HashSet<int> cartProductsId)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("UserProducts",
           JsonConvert.SerializeObject(cartProductsId));
        }
    }
}

