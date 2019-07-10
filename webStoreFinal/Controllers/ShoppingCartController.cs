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

        public ShoppingCartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult ShowCart()
        {
            ViewBag.pageName = "Current Cart Content";
            ViewBag.activeCart = true;
            List<Product> currentCart = _cartService.ShowCart();

            if(currentCart.Count==0)
            {
                ViewBag.emptyMsg = "You havn't any product in your shopping cart...for what are you waiting?";
            }
            else
            {
                double visitorPrice = _cartService.VisitorCartSum(currentCart);
                double memberPrice = _cartService.MemberCartSum(currentCart);

                TempData["visitorPrice"] = visitorPrice;
                TempData["memberPrice"] = memberPrice;
            }
            return View(currentCart);
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


        [HttpPost]//check if to do async
        public IActionResult CompletePurchase()
        {
            ViewBag.pageName = "Purchase Summary";
            _cartService.CompletePurchase();
            return View();
        }

    }
}