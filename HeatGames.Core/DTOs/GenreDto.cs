using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class GenreDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Името на жанра е задължително.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Жанрът трябва да е между 3 и 50 символа.")]
        [Display(Name = "Жанр")]
        public string Name { get; set; } = null!;
    }
}