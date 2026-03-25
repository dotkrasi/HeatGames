using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден формат на имейл адрес.")]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = null!;

        [Range(0, 100000, ErrorMessage = "Невалиден баланс.")]
        [Display(Name = "Баланс в портфейла")]
        public decimal WalletBalance { get; set; }

        [Url(ErrorMessage = "Невалиден URL за профилна снимка.")]
        public string? ProfilePictureUrl { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}