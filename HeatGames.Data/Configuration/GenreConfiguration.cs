using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HeatGames.Data.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            // Изискване от заданието: Инициализация на начални данни чрез Fluent API
            builder.HasData(
                new Genre { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Action" },
                new Genre { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "RPG" },
                new Genre { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Strategy" },
                new Genre { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Shooter" }
            );
        }
    }
}