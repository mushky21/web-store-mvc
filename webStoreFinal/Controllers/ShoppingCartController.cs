using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webStoreFinal.Models;
using webStoreFinal.Services;

namespace webStoreFinal.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductRepository _productRepository;

        public ShoppingCartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult ShowCart()
        {
            HashSet<int> cartProductsId;
            string productsCookiesJson = Request.Cookies["UserProducts"];
            if (string.IsNullOrWhiteSpace(productsCookiesJson))
            {
                cartProductsId = new HashSet<int>();
            }
            else
            {
                cartProductsId = JsonConvert.DeserializeObject<HashSet<int>>(productsCookiesJson);
            }
            List<Product> currentCart = _productRepository.ShowCart(cartProductsId);

            return View(currentCart);//לא ממשנו רק יצרנו את הדף של זה
        }

        public IActionResult RemoveFromCart(int id)
        {
            HashSet<int> cartProductsId;
            string productsCookiesJson = Request.Cookies["UserProducts"];
            cartProductsId = JsonConvert.DeserializeObject<HashSet<int>>(productsCookiesJson);
            cartProductsId.Remove(id);

            Response.Cookies.Append("UserProducts",
            JsonConvert.SerializeObject(cartProductsId),
            new CookieOptions { MaxAge = TimeSpan.FromHours(1) });
            return View("ShowCart", _productRepository.ShowCart(cartProductsId));

        }
    }
}