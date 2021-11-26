using System.ComponentModel.DataAnnotations;

namespace bLibrary.Models.Identity
{
    public class RegisterModel
    {
        [Required, MaxLength(128, ErrorMessage = "Требуется сменить никнейм"), Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
        [Required, Range(1, 100, ErrorMessage = "Задан неверный возраст"), Display(Name = "Возраст")]
        public int Age { get; set; }
        [Required, DataType(DataType.EmailAddress, ErrorMessage = "Задана неверная почта"), Display(Name = "Электронная почта")]
        public string Email { get; set; }
        [Required, MinLength(5, ErrorMessage = "Задан неверный пароль"), DataType(DataType.Password), Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "Пароли не совпадают"), Display(Name = "Повторить пароль")]
        public string ConfirmPassword { get; set; }
    }
    public class LoginModel
    {
        [Required, Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
