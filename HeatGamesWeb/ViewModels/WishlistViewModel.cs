using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGamesWeb.ViewModels
{
    public class WishlistViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string UserId { get; set; } = null!;

        public int GameId { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
    }
}
