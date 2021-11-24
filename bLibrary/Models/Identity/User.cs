using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bLibrary.Models.Identity
{
    public class User : IdentityUser
    {
        [Required, Range(14, 100)]
        public int Age { get; set; }
        [Required]
        public List<Review> Reviews { get; set; }
        public User()
        {
            Reviews = new List<Review>();
        }
    }
}
