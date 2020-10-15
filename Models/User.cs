using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Regulations.Models
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        
    }
}
