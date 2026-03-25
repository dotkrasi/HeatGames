using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public string? UserName { get; set; }

        public Guid GameId { get; set; }
        public string? GameTitle { get; set; }

        [Display(Name = "Препоръчвате ли играта?")]
        public bool IsPositive { get; set; }

        [Required(ErrorMessage = "Коментарът не може да бъде празен.")]
        [StringLength(2000, MinimumLength = 5, ErrorMessage = "Коментарът трябва да е между 5 и 2000 символа.")]
        [Display(Name = "Вашето ревю")]
        public string Comment { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
    }
}