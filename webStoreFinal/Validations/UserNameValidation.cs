using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Models;
using webStoreFinal.Services;

namespace webStoreFinal.Validations
{
    public class UserNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("");
            var userManager = (UserManager<MyUser>)validationContext.GetService(typeof(UserManager<MyUser>));
            string userName = value.ToString();
            var user = userManager.Users.FirstOrDefault(u=>u.UserName==userName);
            // var user = userManager.Users.FirstOrDefault((u) => u.UserName == userName);
            if (user != null)
            {
                return new ValidationResult("Username is already exist,please type again");
            }
            return ValidationResult.Success;
        }

    }
}
