using System.ComponentModel.DataAnnotations;

namespace bLibrary.Models.Identity
{
    public class RegisterModel
    {
        [Required, MaxLength(128, ErrorMessage = "Требуется сменить никнейм")]
        public string Nickname { get; set; }
        [Required, DataType(DataType.EmailAddress, ErrorMessage = "Задана неверная почта")]
        public string Email { get; set; }
        [Required, MinLength(5, ErrorMessage = "Задан неверный пароль"), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
        [Required, Range(18, 100, ErrorMessage = "Задан неверный возраст")]
        public int Age { get; set; }
    }
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
