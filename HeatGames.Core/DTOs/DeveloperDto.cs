using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class DeveloperDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Developer name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        [Display(Name = "Developer Name")]
        public string Name { get; set; } = null!;

        [Url(ErrorMessage = "Please enter a valid URL.")]
        [MaxLength(255)]
        [Display(Name = "Website")]
        public string? Website { get; set; }
    }
}