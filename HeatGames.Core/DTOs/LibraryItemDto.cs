using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class LibraryItemDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        [Display(Name = "Game Title")]
        public string GameTitle { get; set; } = null!;

        public string? CoverImageUrl { get; set; }

        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Play Time (minutes)")]
        public int PlayTimeMinutes { get; set; }
    }
}