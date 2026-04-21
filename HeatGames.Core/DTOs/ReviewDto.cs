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

        [Display(Name = "Do you recommend the game?")]
        public bool IsPositive { get; set; }

        [Required(ErrorMessage = "The comment cannot be empty.")]
        [StringLength(2000, MinimumLength = 5, ErrorMessage = "The comment must be between 5 and 2000 characters.")]
        [Display(Name = "Your review")]
        public string Comment { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
    }
}