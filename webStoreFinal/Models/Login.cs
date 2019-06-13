using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webStoreFinal.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "User name")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
