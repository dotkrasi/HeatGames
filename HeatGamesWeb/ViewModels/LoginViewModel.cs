using System.ComponentModel.DataAnnotations;

namespace HeatGamesWeb.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Въведете потребителско име.")]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Въведете парола.")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; } = null!;

        [Display(Name = "Запомни ме")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}