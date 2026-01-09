using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data.Models
{
    public class Bundles
    {
        [Key]
        public int BundleId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public ICollection<GamesBundles> gamesBundles { get; set; } = new List<GamesBundles>();
    }
}
