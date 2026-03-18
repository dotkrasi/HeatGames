using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGamesWeb.ViewModels
{
    public class GameGenreViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid GameId { get; set; }

        public Guid GenreId { get; set; }
    }
}
