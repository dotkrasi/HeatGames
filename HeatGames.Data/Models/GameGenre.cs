using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data.Models
{
    public class GameGenre
    {
        [Key]
        public Guid Id { get; set; }

        public Guid GameId { get; set; }
        public Game Game { get; set; } = null!;

        public Guid GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
    }
}
