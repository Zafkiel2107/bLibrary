using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace bLibrary.Models
{
    public class Genre
    {
        [Key, Required, HiddenInput(DisplayValue = false)]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено"), StringLength(128, ErrorMessage = "Недопустимая длина строки"), Display(Name = "Жанр")]
        public string GenreName { get; set; }
        [Required]
        public List<Book> Books { get; set; }
        public Genre()
        {
            Books = new List<Book>();
        }
    }
}
