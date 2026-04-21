using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; } = null!;

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        public IEnumerable<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}