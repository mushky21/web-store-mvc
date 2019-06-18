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
        private ICartService _cartService;

        public ShoppingCartController(IProductRepository productRepository, ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult ShowCart()
        {
            List<Product> currentCart = _cartService.ShowCart();

            double visitorPrice = _cartService.VisitorCartSum(currentCart);
            double memberPrice = _cartService.MemberCartSum(currentCart);

            TempData["visitorPrice"] = visitorPrice;
            TempData["memberPrice"] = memberPrice;

            return View(currentCart);//לא ממשנו רק יצרנו את הדף של זה
        }

        public IActionResult RemoveFromCart(int id)
        {
            HashSet<int> cartProductsId = _cartService.RemoveProduct(id);
            return View("ShowCart", _cartService.ShowCart());

        }

        //gets the current item list storaged in cookies and updates it when an item is added
        //goes first to available items action
        [AllowAnonymous]
        public IActionResult AddToCart(int id, State productState)
        {
            if (productState == State.Available)
            {
                _cartService.AddProduct(id);
            }
            else
            {
                ViewBag.ErrorMessage = "sorry, the item is not availalbe at the moment";
            }
            return RedirectToAction("AvailableItems", "Home");

        }


        [HttpPost]
        public IActionResult CompletePurchase()
        {
            if(User.Identity.IsAuthenticated) 
            _cartService.CompletePurchase();
            return RedirectToAction("ShowCart");
        }

    }
}