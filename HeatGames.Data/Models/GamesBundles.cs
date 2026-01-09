using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HeatGames.Data.Models
{
    public class GamesBundles
    {
        [Required]
        public int Id { get; set; }

        [ForeignKey(nameof(Games))]
        public int gameId { get; set; }
        public Games Game { get; set; }

        [ForeignKey(nameof(Bundles))]
        public int bundleId { get; set; }
        public Bundles Bundles { get; set; }


    }
}
