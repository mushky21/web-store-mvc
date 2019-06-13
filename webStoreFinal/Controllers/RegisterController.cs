using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webStoreFinal.Models;
using webStoreFinal.Services;

namespace webStoreFinal.Controllers
{
    public class RegisterController : Controller
    {
        private IUserRepository _userRepository;
        public RegisterController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //navigated to view of registration if user is not authenticated
        //else navigated to view of updating details
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return View("Register");
            MyUser user = _userRepository.FindUserAuthenticated();
            return View("UpdatingDetails",);
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register registerData)
        {
            if (ModelState.IsValid)
            {
                var result =await _userRepository.AddUser(registerData);
                if (result.Succeeded)
                {
                    return RedirectToAction("AvailableItems", "Home");
                }
                else
                {
                    var error = string.Join(", ", result.Errors.Select(e => e.Description));//another option to build reg for password and define reg in configure services of identity
                    ModelState.AddModelError("Password", error);

                }
            }
            return View("Register");
        }

        [HttpGet]
        public IActionResult UpdatingDetails(MyUser myUser)
        {
            _userRepository.UpdateUser(myUser);
        }
    }
}