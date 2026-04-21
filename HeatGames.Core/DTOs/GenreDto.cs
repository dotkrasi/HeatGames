using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class GenreDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The genre name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The genre must be between 3 and 50 characters.")]
        [Display(Name = "Genre")]
        public string Name { get; set; } = null!;
    }
}