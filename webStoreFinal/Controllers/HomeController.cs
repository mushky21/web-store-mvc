﻿using System;
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
        [AllowAnonymous]
        public IActionResult AvailableItems()
        {
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
            return View(_productRepository.FindProduct(id));
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