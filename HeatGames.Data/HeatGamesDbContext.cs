using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data
{
    public class HeatGamesDbContext : DbContext
    {
        public HeatGamesDbContext(DbContextOptions<HeatGamesDbContext> options) : base(options)
        {
        }
        public DbSet<Games> Games { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Bundles> Bundles { get; set; }
        public DbSet<GamesBundles> GamesBundles { get; set; }
        public DbSet<UserGames> UserGames { get; set; }
    }
}
