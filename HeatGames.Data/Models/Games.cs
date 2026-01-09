using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data.Models
{
    public class Games
    {

        [Key]
        public int GameId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Developer { get; set; }

        [Required]
        [MaxLength(50)]
        public string Publisher { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Rating { get; set; }

        [ForeignKey(nameof(Genre))]
        public int genreId { get; set; }
        public Genres Genre { get; set; }


        // 1:N Games ↔ Reviews
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();

        // M:N Games ↔ Users (Library)
        public ICollection<UserGames> Users { get; set; } = new List<UserGames>();

        // M:N Games ↔ Bundles
        public ICollection<GamesBundles> gamesBundles { get; set; } = new List<GamesBundles>();
    }
}

