using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGamesWeb.ViewModels
{
    public class ReviewViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        public int GameId { get; set; }

        public bool IsPositive { get; set; } // true за Like (Thumbs Up), false за Dislike

        [Required]
        [MaxLength(2000)]
        public string Comment { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
