using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data.Models
{
    public class Genres
    {

        [Key]
        public int GenreId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Games> Games { get; set; } = new List<Games>();



    }
}
