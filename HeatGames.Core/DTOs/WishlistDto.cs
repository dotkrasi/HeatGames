using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class WishlistDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        [Display(Name = "Game Title")]
        public string GameTitle { get; set; } = null!;

        [Display(Name = "Current Price")]
        public decimal CurrentPrice { get; set; }

        public string? CoverImageUrl { get; set; }

        [Display(Name = "Added On")]
        public DateTime AddedOn { get; set; }
    }
}