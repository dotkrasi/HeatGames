using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGamesWeb.ViewModels
{
    public class GameViewModel
    {
        // Не ни трябва [Key] тук
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Полето 'Заглавие' е задължително.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Заглавието трябва да е между 2 и 150 символа.")]
        [Display(Name = "Заглавие на играта")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Описание' е задължително.")]
        [Display(Name = "Описание")]
        public string Description { get; set; } = null!;

        // Премахваме [Column], слагаме [Range] за валидация
        [Required(ErrorMessage = "Моля, въведете цена.")]
        [Range(0.00, 10000.00, ErrorMessage = "Цената трябва да бъде положително число.")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Посочете дата на излизане.")]
        [Display(Name = "Дата на излизане")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [MaxLength(255)]
        [Display(Name = "URL на корицата")]
        public string? CoverImageUrl { get; set; }

        [Required(ErrorMessage = "Моля, изберете разработчик.")]
        [Display(Name = "Разработчик")]
        public Guid DeveloperId { get; set; }

        [Display(Name = "Платформи")]
        public List<Guid> SelectedPlatformIds { get; set; } = new();
        public IEnumerable<string> Platforms { get; set; } = new List<string>();
        public IEnumerable<string> Genres { get; set; } = new List<string>(); // Добави и това, ако го няма
    }
}

