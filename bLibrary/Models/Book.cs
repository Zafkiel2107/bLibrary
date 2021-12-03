using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace bLibrary.Models
{
    public class Book
    {
        [Key, Required, HiddenInput(DisplayValue = false)]
        public int BookId { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено"), StringLength(128, ErrorMessage = "Недопустимая длина строки"), Display(Name = "Название книги")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено"), StringLength(128, ErrorMessage = "Недопустимая длина строки"), Display(Name = "Автор")]
        public string Author { get; set; }
        [Required, Display(Name = "Количество рекомендаций")]
        public int RecommendationsNum { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено"), Display(Name = "Жанр")]
        public Genre Genre { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено"), Display(Name = "Часть")]
        public int Part { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено"), Display(Name = "Страниц")]
        public int Pages { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено"), Display(Name = "Язык")]
        public Language Language { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено"), StringLength(1024, ErrorMessage = "Недопустимая длина строки"), Display(Name = "Описание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено"), Display(Name = "Ссылка на обложку")]
        public string CoverLink { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено"), Display(Name = "Ссылка на книгу")]
        public string BookLink { get; set; }
        [Required, Display(Name = "Отзывы")]
        public List<Review> Reviews { get; set; }
        public Book()
        {
            RecommendationsNum = 0;
            Reviews = new List<Review>();
        }
    }
    public enum Language : byte
    {
        Русский,
        Английский
    }
}