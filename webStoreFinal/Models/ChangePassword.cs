using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Validations;

namespace webStoreFinal.Models
{
    public class ChangePassword
    {
        [Required]
        [IsCurrentPassword]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Re-type Password")]
        [Compare(nameof(NewPassword), ErrorMessage = "Please Type Again")]
        public string PasswordConfirm { get; set; }
    }
}
