using System.ComponentModel.DataAnnotations;

namespace Kyrsova.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email обязателен.")]
        [EmailAddress(ErrorMessage = "Некорректный формат Email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имя обязательно.")]
        [MinLength(1, ErrorMessage = "Имя должно содержать хотя бы 1 символ.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилия обязательна.")]
        [MinLength(1, ErrorMessage = "Фамилия должна содержать хотя бы 1 символ.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефон обязателен.")]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Некорректный формат номера телефона.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Подтверждение пароля обязательно.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
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
