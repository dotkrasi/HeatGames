using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [Display(Name = "Потребител")]
        public string UserName { get; set; } = null!;

        [Display(Name = "Дата на поръчката")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Обща сума")]
        public decimal TotalAmount { get; set; }

        public IEnumerable<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}