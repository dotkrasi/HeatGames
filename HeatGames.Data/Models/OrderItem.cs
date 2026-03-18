using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data.Models
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAtPurchase { get; set; }
    }
}
