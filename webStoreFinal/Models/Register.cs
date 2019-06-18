using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Validations;

namespace webStoreFinal.Models
{
    public class Register
    {
        [UserNameValidation]
        [Required]
        [Display(Name = "User name")]
        //check if username is exist
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Re-type Password")]
        [Compare(nameof(Password), ErrorMessage = "Please Type Again")]
        public string PasswordConfirm { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
