using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Range(0, 100000, ErrorMessage = "Invalid balance.")]
        [Display(Name = "Wallet Balance")]
        public decimal WalletBalance { get; set; }

        [Url(ErrorMessage = "Invalid profile picture URL.")]
        public string? ProfilePictureUrl { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}