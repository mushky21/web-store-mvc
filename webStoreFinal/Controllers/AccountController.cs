using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webStoreFinal.Models;

namespace webStoreFinal.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<MyUser> _signInManager;

        public AccountController(SignInManager<MyUser> signInManager)
        {
            _signInManager = signInManager;

        }

        //when login is ended
        //[HttpPost]
        //public async Task<IActionResult> Login(Login login)
        //{
        //    var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);
        //    if (result == Microsoft.AspNetCore.Identity.SignInResult.Success)
        //    {
        //        string userName = login.Username;
        //        Response.Cookies.Append(login.Username, login.Password);//save login details in cookies
        //        //need to return to layout by firstname and last name of user and button for logout

        //    }
        //    else
        //    {
        //        //need to show error message of "the user name or password are incorrect"
        //    }
        //}
    }
}