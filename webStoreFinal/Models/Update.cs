using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Validations;

namespace webStoreFinal.Models
{
    public class Update:Register
    {
        [Required]
        [IsCurrentPassword]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public override string Password { get; set; }
    }
}
