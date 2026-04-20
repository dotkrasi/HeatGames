using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HeatGames.Data.Configuration
{
    public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> builder)
        {
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasData(
                new Platform { Id = Guid.Parse("11111111-2222-3333-4444-555555555555"), Name = "PC (Windows)" },
                new Platform { Id = Guid.Parse("22222222-3333-4444-5555-666666666666"), Name = "PlayStation 5" },
                new Platform { Id = Guid.Parse("33333333-4444-5555-6666-777777777777"), Name = "Xbox Series X/S" },
                new Platform { Id = Guid.Parse("44444444-5555-6666-7777-888888888888"), Name = "Nintendo Switch" }
            );
        }
    }
}