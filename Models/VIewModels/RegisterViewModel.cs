using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Regulations.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required (ErrorMessage = "TypeEmail")]
        public string Email { get; set; }
        [Required (ErrorMessage = "TypePassword")]
        [DataType (DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "WrongPassword")]
        public string ConfirmPassword { get; set; }
        

    }
}
