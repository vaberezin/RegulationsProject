﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Regulations.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }
        //public string NickName { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Role { get; set; }
        
    }
}
