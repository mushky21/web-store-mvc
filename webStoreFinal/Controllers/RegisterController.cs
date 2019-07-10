using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Register"); }

            MyUser userAuthenicated = await _userRepository.FindUserAuthAsync();
            Register currentUserData = new Register
            {
                BirthDate = userAuthenicated.BirthDate,
                Email = userAuthenicated.Email,
                FirstName = userAuthenicated.FirstName,
                LastName = userAuthenicated.LastName,
                Username = userAuthenicated.UserName
            };

            ViewBag.pageName = "Updating Page";
            ViewBag.activeRegister = true;
            return View("Update", currentUserData);
        }

        public IActionResult Register()
        {
            ViewBag.pageName = "Registration Page";
            ViewBag.activeRegister = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register registerData)
        {          
            if (ModelState.IsValid)
            {
                var result = await _userRepository.AddUser(registerData);
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
            ViewBag.activeRegister = true;
            ViewBag.pageName = "Registration Page";
            return View("Register");
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatingDetails(Register updateData)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.UpdateUser(updateData);
                if (result.Succeeded)
                {
                    return RedirectToAction("AvailableItems", "Home");
                }
            }
            ViewBag.pageName = "Updating Page";
            return View("Update", updateData);         
        }

        [Authorize]
        public IActionResult UpdateNewPassword()
        {
            ViewBag.pageName = "Updating new password";
            return View();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateNewPassword(ChangePassword changeData)
        {
            //update password
            string newPass = changeData.NewPassword;
            await _userRepository.UpdatePassword(newPass);
            return RedirectToAction("AvailableItems", "Home");
        }
    }
}