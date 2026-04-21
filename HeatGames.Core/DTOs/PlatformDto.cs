using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class PlatformDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The platform name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The platform name must be between 2 and 50 characters.")]
        [Display(Name = "Platform")]
        public string Name { get; set; } = null!;
    }
}