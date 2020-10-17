using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Regulations.Models
{
    public class Regulation
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Link { get; set; }
        public DateTime Added { get; set; }
        //public User Author { get; set; }
        public int UserId { get; set; }

        

    }
}
