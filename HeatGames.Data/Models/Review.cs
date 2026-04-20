using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid GameId { get; set; }
        public Game Game { get; set; } = null!;

        public bool IsPositive { get; set; } // true за Like, false за Dislike

        [Required]
        [MaxLength(2000)]
        public string Comment { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
