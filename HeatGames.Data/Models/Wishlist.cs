using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data.Models
{
    public class Wishlist
    {
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
    }
}
