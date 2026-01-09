using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data.Models
{
    public class Users
    {

        [Key]

        public int UserId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        public string Password { get; set; }

        [Required]
        public double Balance { get; set; }


        // M:N Users ↔ Games (Library)
        public ICollection<UserGames> Library { get; set; } = new List<UserGames>();

        // 1:N Users ↔ Reviews
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
    }
}
