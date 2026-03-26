using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class LibraryItemDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        [Display(Name = "Заглавие на играта")]
        public string GameTitle { get; set; } = null!;

        public string? CoverImageUrl { get; set; }

        [Display(Name = "Дата на закупуване")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Изиграно време (минути)")]
        public int PlayTimeMinutes { get; set; }
    }
}