using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data.Models
{
    public class LibraryItem
    {
        [Key]
        public Guid Id { get; set; }

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        public DateTime PurchaseDate { get; set; }
        public int PlayTimeMinutes { get; set; } = 0;
    }
}
