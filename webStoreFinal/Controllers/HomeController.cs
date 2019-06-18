using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webStoreFinal.Models;
using webStoreFinal.Services;

namespace webStoreFinal.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //public IActionResult Index(string key) //shows the items according to the wanted order by method.
        //{
        //    List<Product> myProductsList = new List<Product>();
        //    //if (TempData.ContainsKey(key))
        //    //{
        //    //    myProductsList
        //    //        = JsonConvert.DeserializeObject<List<Product>>(TempData[key].ToString());
        //    //}
        //    return View(myProductsList);
        //}

        public IActionResult AvailableItems()
        {
            if (TempData["LoginFailed"] != null) ViewBag.LoginFailed = TempData["LoginFailed"];
            return View("Index", _productRepository.AvailableItems());
        }

        public IActionResult OrderByDate()
        {
            return View("Index", _productRepository.OrderByDate());
        }

        public IActionResult OrderByTitle()
        {
            return View("Index", _productRepository.OrderByTitle());
        }

        public IActionResult ShowDetails(int id)
        {
            return View(_productRepository.FindProduct(id));
        }

        //gets the current item list storaged in cookies and updates it when an item is added
        //goes first to available items action
        public IActionResult AddToCart(int id, State productState)
        {
            if (productState == State.Available)
            {
                _productRepository.UpdateProductState(id);

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

                cartProductsId.Add(id);
                Response.Cookies.Append("UserProducts",
                    JsonConvert.SerializeObject(cartProductsId),
                    new CookieOptions { MaxAge = TimeSpan.FromHours(1) });
            }
            else
            {
                ViewBag.ErrorMessage = "sorry, the item is not availalbe at the moment";
            }
            return RedirectToAction("AvailableItems", "Home");

        }

        public IActionResult AddNewAdvertisement()
        {
            return View();//navigated to AddNewAdvertisement view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewAdvertisement(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.AddProduct(product);
                ViewBag.ItemAdded = "the item was published successfully";
            }
            return View();//navigated to AddNewAdvertisement view

        }


    }
}