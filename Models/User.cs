using System.ComponentModel.DataAnnotations;

namespace Kyrsova.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email обов'язковий до заповнення")]
        [EmailAddress(ErrorMessage = "Некорректный формат Email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ім'я обов'язкове до заповнення")]
        [MaxLength(15, ErrorMessage = "Ім'я неповинно мати більше за 15 символів!")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Призвище обов'язкове до заповнення")]
        [MaxLength(15, ErrorMessage = "Призвище неповинно мати більше за 15 символів!")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефон обязателен.")]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Некорректный формат номера телефона.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен.")]
        [DataType(DataType.Password)]
        [MaxLength(21, ErrorMessage = "Пароль не може бути довший за 20 символів.")]
        [MinLength(8, ErrorMessage = "Заннадто короткий пароль!")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Це поле обов'язкове до заповнення")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Email обязателен.")]
        [EmailAddress(ErrorMessage = "Некорректный формат Email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
