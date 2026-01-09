using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data.Models
{
    public class Reviews
    {

        [Key]
        public int ReviewId { get; set; }

        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }
        public Users Users { get; set; }

        [ForeignKey(nameof(Games))]
        public int GameId { get; set; }
        public Games Games { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public DateTime dateTime { get; set; }

    }
}
