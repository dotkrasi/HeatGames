using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class DeveloperDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Името на разработчика е задължително.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Името трябва да е между 2 и 100 символа.")]
        [Display(Name = "Име на разработчик")]
        public string Name { get; set; } = null!;

        [Url(ErrorMessage = "Моля, въведете валиден URL адрес.")]
        [MaxLength(255)]
        [Display(Name = "Уебсайт")]
        public string? Website { get; set; }
    }
}