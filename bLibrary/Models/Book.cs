using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace bLibrary.Models
{
    public class Book
    {
        [Key, Required, HiddenInput(DisplayValue = false)]
        public int BookId { get; set; }
        [Required, StringLength(128), Display(Name = "Название книги")]
        public string Name { get; set; }
        [Required, StringLength(128), Display(Name = "Автор")]
        public string Author { get; set; }
        [Required, Display(Name = "Количество рекомендаций")]
        public int RecommendationsNum { get; set; }
        [Required, Display(Name = "Жанры")]
        public List<Genre> Genres { get; set; }
        [Required, Display(Name = "Часть")]
        public int Part { get; set; }
        [Required, Display(Name = "Страниц")]
        public int Pages { get; set; }
        [Required, Display(Name = "Язык")]
        public Language Language { get; set; }
        [Required, StringLength(1024), Display(Name = "Описание")]
        public string Description { get; set; }
        [Required]
        public List<Review> Reviews { get; set; }
    }
    public enum Genre : byte
    {

    }
    public enum Language : byte
    {
        Русский,
        Английский
    }
}