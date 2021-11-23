using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bLibrary.Models.Identity
{
    public class User : IdentityUser
    {
        [Required, MaxLength(128, ErrorMessage = "Требуется сменить никнейм"), Display(Name = "Никнейм")]
        public string Nickname { get; set; }
        [Required, MinLength(5, ErrorMessage = "Задан неверный пароль"), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, Range(18, 100)]
        public int Age { get; set; }
        [Required]
        public List<Review> Reviews { get; set; }
    }
}
