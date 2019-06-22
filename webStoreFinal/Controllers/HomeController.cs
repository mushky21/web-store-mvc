using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webStoreFinal.Models;
using webStoreFinal.Services;

namespace webStoreFinal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IProductRepository _productRepository;
        private IUserRepository _userRepository;
    

        public HomeController(IProductRepository productRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
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

        [AllowAnonymous]
        public IActionResult AvailableItems()
        {
            ViewBag.pageName = "HOME PAGE";
            if (TempData["LoginError"] != null) ViewBag.LoginError = TempData["LoginError"];
            return View("Index", _productRepository.AvailableItems());
        }

        [AllowAnonymous]
        public IActionResult OrderByDate()
        {
            return View("Index", _productRepository.OrderByDate());
        }

        [AllowAnonymous]
        public IActionResult OrderByTitle()
        {
            return View("Index", _productRepository.OrderByTitle());
        }

        [AllowAnonymous]
        public IActionResult ShowDetails(int id)
        {
            ViewBag.pageName = "More Details";
            return View(_productRepository.FindProduct(id));
        }

        //a method only authorized users can have access to
        //   suggestion:     [Authorize(Roles ="SignedInUser")]
        public IActionResult AddNewAdvertisement()
        {
            ViewBag.pageName = "Add New Advertisement";
            return View();//navigated to AddNewAdvertisement view
        }

        //a method only authorized users can have access to
        [HttpPost]
        [ValidateAntiForgeryToken]
        //    suggestion:    [Authorize(Roles ="SignedInUser")]
        public async Task<IActionResult> AddNewAdvertisement(Product product, IFormFile[]pictures)
        {
            ViewBag.pageName = "Add New Advertisement";
            if (ModelState.IsValid)
            {
                MyUser userAuthenticated = await _userRepository.FindUserAuthenticated();
                product.SellerId = userAuthenticated.Id;
                if (pictures.Length <3)
                {
                   await _productRepository.AddProduct(product, pictures);
                    ViewBag.ItemAdded = "the item was published successfully";
                }
                else
                {
                    ViewBag.PictureError = "please enter up to 3 pictures only";
                }
                

            }
            return View();

        }


    }
}