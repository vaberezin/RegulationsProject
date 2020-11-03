using System;
using System.ComponentModel.DataAnnotations;

namespace Regulations.Models
{
    public class Regulation
    {


        [Required] 
        public int Id { get; set; }
        [Required (ErrorMessage =  "strNoRegCrypt")] 
        public string ShortName { get; set; }
        [Required (ErrorMessage = "strNoName")] 
        [StringLength (25, MinimumLength = 2, ErrorMessage = "strStrLengthError")]
        public string FullName { get; set; }
        [Required (ErrorMessage = "strNoLink")]
        [Url (ErrorMessage = "strNotLink")]        
        public string Link { get; set; }
        [Required] 
        public DateTime Added { get; set; }
        //public User Author { get; set; }
        [Required (ErrorMessage = "strNoId")] 

        
        public User User { get; set; } //navigational prop

        [Range (1,1000, ErrorMessage = "strNotYourId")]
        public int UserId { get; set; } //foreign key

        

    }
}
