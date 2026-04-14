using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }

        [Display(Name = "Потребителско име")]
        public string Username { get; set; } = null!;

        [EmailAddress]
        [Display(Name = "Имейл адрес")]
        public string Email { get; set; } = null!;

        [Display(Name = "URL на профилна снимка")]
        public string? ProfilePictureUrl { get; set; }

        [Display(Name = "Баланс в портфейла")]
        public decimal WalletBalance { get; set; }

        [Display(Name = "Дата на регистрация")]
        public DateTime RegistrationDate { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Сегашна парола")]
        public string? CurrentPassword { get; set; }

        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Нова парола")]
        public string? NewPassword { get; set; }
    }
}