using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace bLibrary.Models
{
    public class Genre
    {
        [Key, Required, HiddenInput(DisplayValue = false)]
        public int GenreId { get; set; }
        [Required, StringLength(128)]
        public string GenreName { get; set; }
        public List<Book> Books { get; set; }
        public Genre()
        {
            Books = new List<Book>();
        }
    }
}
