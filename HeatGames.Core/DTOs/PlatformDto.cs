using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class PlatformDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Името на платформата е задължително.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Платформата трябва да е между 2 и 50 символа.")]
        [Display(Name = "Платформа")]
        public string Name { get; set; } = null!;
    }
}