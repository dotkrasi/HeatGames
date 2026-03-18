using HeatGames.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data
{
    public class HeatGamesDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public HeatGamesDbContext(DbContextOptions<HeatGamesDbContext> options) : base(options)
        {
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
