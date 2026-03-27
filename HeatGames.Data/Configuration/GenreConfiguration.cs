using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HeatGames.Data.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(
                new Genre { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Екшън (Action)" },
                new Genre { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "RPG (Ролеви)" },
                new Genre { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Шутър (Shooter)" },
                new Genre { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Стратегия (Strategy)" },
                new Genre { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Приключенски (Adventure)" },
                new Genre { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Независими (Indie)" }
            );
        }
    }
}