using System;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }

        public Guid GameId { get; set; }

        [Display(Name = "Заглавие на играта")]
        public string GameTitle { get; set; } = null!;

        [Display(Name = "Цена при закупуване")]
        public decimal PriceAtPurchase { get; set; }
    }
}