using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Regulations.Models
{
    public class Regulation
    {


        [Required] 
        public int Id { get; set; }
        [Required (ErrorMessage =  "Не указан шифр нормы")] 
        public string ShortName { get; set; }
        [Required (ErrorMessage = "Не указано наименование нормы")] 
        [StringLength (25, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 25 символов")]
        public string FullName { get; set; }
        [Required (ErrorMessage = "Не указана ссылка на норму")]
        [Url (ErrorMessage = "Это не ссылка!")]        
        public string Link { get; set; }
        [Required] 
        public DateTime Added { get; set; }
        //public User Author { get; set; }
        [Required (ErrorMessage = "Не указан Ваш Id")] 

        
        public User User { get; set; } //navigational prop

        [Range (1,1000, ErrorMessage = "Сударь, не лукавьте ;), это не Ваш Id")]
        public int UserId { get; set; } //foreign key

        

    }
}
