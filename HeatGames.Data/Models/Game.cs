using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data.Models
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        [MaxLength(255)]
        public string? CoverImageUrl { get; set; }

        public Guid DeveloperId { get; set; }
        public Developer Developer { get; set; } = null!;

        public ICollection<GameGenre> GameGenres { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<GamePlatform> GamePlatforms { get; set; }
    }
}

