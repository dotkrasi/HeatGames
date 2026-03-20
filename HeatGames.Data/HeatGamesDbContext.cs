using HeatGames.Data.Configuration;
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
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<LibraryItem> LibraryItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        // Тук ще "закачим" нашите Fluent API конфигурации!
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // По-късно тук ще добавим конфигурациите
            builder.ApplyConfigurationsFromAssembly(typeof(HeatGamesDbContext).Assembly);
        }
    }
}
