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
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return View("Register");
            MyUser userAuthenicated = await _userRepository.FindUserAuthenticated();

            Update currentUserData = new Update
            {
                BirthDate= userAuthenicated.BirthDate,
                CurrentPassword=userAuthenicated.PasswordHash,
                Email=userAuthenicated.Email,
                FirstName=userAuthenicated.FirstName,
                LastName=userAuthenicated.LastName,
                Username=userAuthenicated.UserName         
            };

            return View("UpdatingDetails",userAuthenicated);
        }

        public IActionResult Register()
        {
            ViewBag.pageName = "Registration Page";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register registerData)
        {
            ViewBag.pageName = "Registration Page";
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

        [HttpPut]
        public async Task<IActionResult> UpdatingDetails(Update updateData)
        {
            var result = await _userRepository.UpdateUser(updateData);
            if(result.Succeeded)
            {
                return RedirectToAction("AvailableItems", "Home");
            }
            return View("UpdatingDetails", updateData);//need?         
        }
    }
}