using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bLibrary.Models.Identity
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Поле должно быть заполнено"), Range(0, 100, ErrorMessage = "Возраст не может быть больше 100 или меньше 0 лет")]
        public int Age { get; set; }
        [Required]
        public List<Review> Reviews { get; set; }
        public User()
        {
            Reviews = new List<Review>();
        }
    }
}
