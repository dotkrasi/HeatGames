using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }

        public Guid GameId { get; set; }

        [Display(Name = "Game Title")]
        public string GameTitle { get; set; } = null!;

        [Display(Name = "Price at Purchase")]
        public decimal PriceAtPurchase { get; set; }
    }
}