using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private ICartService _cartService;

        public ShoppingCartController(IProductRepository productRepository, ICartService cartService)
        {
            _productRepository = productRepository;
            _cartService = cartService;
        }

        public IActionResult ShowCart()
        {
            List<Product> currentCart = _productRepository.ShowCart(_cartService.ProductsInCookies());
            return View(currentCart);//לא ממשנו רק יצרנו את הדף של זה
        }

        public IActionResult RemoveFromCart(int id)
        {
            HashSet<int> cartProductsId = _cartService.RemoveProductFromCookie(id);
            _productRepository.UpdateProductState(id,State.Available);

            return View("ShowCart", _productRepository.ShowCart(cartProductsId));

        }

        //gets the current item list storaged in cookies and updates it when an item is added
        //goes first to available items action
        [AllowAnonymous]
        public IActionResult AddToCart(int id, State productState)
        {
            if (productState == State.Available)
            {
                _productRepository.UpdateProductState(id,State.InCart);
                _cartService.AddProductToCookie(id);
            }
            else
            {
                ViewBag.ErrorMessage = "sorry, the item is not availalbe at the moment";
            }
            return RedirectToAction("AvailableItems", "Home");

        }

       
        //[HttpPost]
        //public IActionResult CompletePurchase()
        //{

        //    double visitorPrice = _cartService.VisitorCartSum(prices);
        //    double memberPrice = _cartService.MemberCartSum(prices);

        //    TempData["visitorPrice"] = visitorPrice;
        //    TempData["memberPrice"] = memberPrice;


        //    return RedirectToAction("ShowCart");
        //}

    }
}