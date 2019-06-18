﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webStoreFinal.Models;
using SignInResult = Microsoft.AspNetCore.Mvc.SignInResult;

namespace webStoreFinal.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<MyUser> _signInManager;

        public AccountController(SignInManager<MyUser> signInManager)
        {
            _signInManager = signInManager;

        }

        public async Task<IActionResult> Login(Login login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password,true, false);
            if (result.Succeeded)
            {
                return RedirectToAction("AvailableItems", "Home");

            }
            else
            {
                return RedirectToAction("AvailableItems", "Home");//??

                //need to show error message of "the user name or password are incorrect"
            }
        }
    }
}