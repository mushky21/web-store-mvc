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
    public class HomeController : Controller
    {
        private IProductRepository _productRepository;
        private IUserRepository _userRepository;
        private SignInManager<MyUser> _signInManager;

      /*  private static bool isFirstVisit = true;*///check if it is first visit in this page, for this browsing


        public HomeController(IProductRepository productRepository, IUserRepository userRepository, SignInManager<MyUser> signInManager)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _signInManager = signInManager;
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
            ////try sign in user, if his username is exist in cookies
            ////only, at the first time, user enter to this page, for each browsing
            //if (isFirstVisit)
            //{
            //    isFirstVisit = true;

            //    Login userLogin = await _userRepository.RecognizeUser();
            //    if (userLogin != null)
            //        RedirectToAction("Login", "Account", userLogin);//sign in user
            //}

            ViewBag.pageName = "HOME PAGE";
            if (TempData["LoginError"] != null) ViewBag.LoginError = TempData["LoginError"];
            return View("Index", _productRepository.AvailableItems());
        }

        public IActionResult OrderByDate()
        {
            ViewBag.pageName = "HOME PAGE";
            return View("Index", _productRepository.OrderByDate());
        }

        public IActionResult OrderByTitle()
        {
            ViewBag.pageName = "HOME PAGE";
            return View("Index", _productRepository.OrderByTitle());
        }

        public IActionResult ShowDetails(int id)
        {
            var product = _productRepository.FindProduct(id);
            if (product==null)
            {
                return RedirectToAction("ShowError", "Error");
            }
            ViewBag.pageName = "More Details";
            return View("ShowDetails", product);
 

        }

        //a method only authorized users can have access to
        //   suggestion:     [Authorize(Roles ="SignedInUser")]
        [Authorize]
        public IActionResult AddNewAdvertisement()
        {
            ViewBag.pageName = "Add New Advertisement";
            return View();//navigated to AddNewAdvertisement view
        }
        [Authorize]
        //a method only authorized users can have access to
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddNewAdvertisement(Product product, IFormFile[] pictures)
        {
            bool isAdded = default(bool) ;
            ViewBag.pageName = "Add New Advertisement";
            if (ModelState.IsValid)
            {
                MyUser userAuthenticated = await _userRepository.FindUserAuthAsync();
                product.SellerId = userAuthenticated.Id;
               
                if (pictures.Length > 3)
                {
                    ViewBag.PictureError = "please enter up to 3 pictures only";
                }
                else if (pictures.Length < 3)
                {
                    isAdded = await _productRepository.AddProduct(product, pictures);
                    if (isAdded)
                    {
                        ViewBag.isAdded = isAdded;
                        ViewBag.ItemAdded = "the item was published successfully";
                    }
                 
                }


            }
            ;
            return View();

        }


    }
}