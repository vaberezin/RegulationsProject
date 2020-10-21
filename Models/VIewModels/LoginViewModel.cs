﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Regulations.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required (ErrorMessage = "Не указан e-mail.")]
        public string Email { get; set; }
        [Required (ErrorMessage = "Не указан пароль.")]
        [DataType (DataType.Password)]
        public string Password { get; set; }            

    }
}
