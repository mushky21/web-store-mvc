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
        private static bool isFirstVisit = true;//check if it is first visit in this page, for this browsing


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
        public async Task<IActionResult> AvailableItems()
        {
            //try sign in user, if his username is exist in cookies
            //only, at the first time, user enter to this page, for each browsing
            if (isFirstVisit)
            {
                isFirstVisit = true;

                Login userLogin = await _userRepository.RecognizeUser();
                if (userLogin != null)
                    RedirectToAction("Login", "Account", userLogin);//sign in user
            }

            ViewBag.pageName = "HOME PAGE";
            if (TempData["LoginError"] != null) ViewBag.LoginError = TempData["LoginError"];
            return View("Index", _productRepository.AvailableItems());
        }

        [AllowAnonymous]
        public IActionResult OrderByDate()
        {
            ViewBag.pageName = "HOME PAGE";
            return View("Index", _productRepository.OrderByDate());
        }

        [AllowAnonymous]
        public IActionResult OrderByTitle()
        {
            ViewBag.pageName = "HOME PAGE";
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
        public async Task<IActionResult> AddNewAdvertisement(Product product, IFormFile[] pictures)
        {
            ViewBag.pageName = "Add New Advertisement";
            if (ModelState.IsValid)
            {
                MyUser userAuthenticated = await _userRepository.FindUserAuthenticated();
                product.SellerId = userAuthenticated.Id;
                if (pictures.Length < 3)
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