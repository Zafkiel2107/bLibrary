using bLibrary.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace bLibrary.Models
{
    public class Review
    {
        [Key, Required, HiddenInput(DisplayValue = false)]
        public int ReviewId { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено"), Display(Name = "Статус")]
        public Status Status { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено"), StringLength(1024, ErrorMessage = "Недопустимая длина строки"), Display(Name = "Отзыв")]
        public string UserReview { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено"), Display(Name = "Рекомендация")]
        public bool IsRecommended { get; set; }
        [DataType(DataType.Date), Display(Name = "Дата написания")]
        public DateTime CreateDate { get; set; }
        [Required]
        public Book Book { get; set; }
        [Required]
        public User User { get; set; }
        public Review()
        {
            this.CreateDate = DateTime.Now;
        }
    }
    public enum Status : byte
    {
        Буду_читать,
        Читаю,
        Прочитана,
        Не_дочитана
    }
}