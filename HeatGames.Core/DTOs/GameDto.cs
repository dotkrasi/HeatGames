using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class GameDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Полето 'Заглавие' е задължително.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Заглавието трябва да е между 2 и 150 символа.")]
        [Display(Name = "Заглавие")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Описание' е задължително.")]
        [MinLength(10, ErrorMessage = "Описанието трябва да е поне 10 символа.")]
        [Display(Name = "Описание")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Моля, въведете цена.")]
        [Range(0.00, 10000.00, ErrorMessage = "Цената трябва да бъде положително число.")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Посочете дата на излизане.")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата на излизане")]
        public DateTime ReleaseDate { get; set; }

        [Url(ErrorMessage = "Моля, въведете валиден URL за снимката.")]
        [MaxLength(255)]
        [Display(Name = "URL на корицата")]
        public string? CoverImageUrl { get; set; }

        [Required(ErrorMessage = "Изборът на разработчик е задължителен.")]
        [Display(Name = "Разработчик")]
        public Guid DeveloperId { get; set; }

        public string? DeveloperName { get; set; }
        public IEnumerable<string> Genres { get; set; } = new List<string>();
        public IEnumerable<string> Platforms { get; set; } = new List<string>();

        public List<Guid> SelectedPlatformIds { get; set; } = new();
        public List<Guid> SelectedGenreIds { get; set; } = new List<Guid>();
    }
}