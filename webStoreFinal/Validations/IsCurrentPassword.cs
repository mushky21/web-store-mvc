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
    public class IsCurrentPassword : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("");

            var passwordHasher = (IPasswordHasher<MyUser>)validationContext.GetService(typeof(IPasswordHasher<MyUser>));
            var userRepo = (IUserRepository)validationContext.GetService(typeof(IUserRepository));

            string currentPassword = value.ToString();
            var userAuth = userRepo.FindUserAuth();

            if (passwordHasher.VerifyHashedPassword(userAuth,userAuth.PasswordHash.ToString(),currentPassword)== PasswordVerificationResult.Success)
            {
                return ValidationResult.Success;
               
            }
            return new ValidationResult("Current password is not correct");

        }
    }
}
