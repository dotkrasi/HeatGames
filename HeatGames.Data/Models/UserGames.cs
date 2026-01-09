using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeatGames.Data.Models
{
    public class UserGames
    {

        [Required]
        public int Id { get; set; }

        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }
        public Users user { get; set; }

        [ForeignKey(nameof(Games))]
        public int GameId { get; set; }
        public Games game { get; set; }

    }
}
