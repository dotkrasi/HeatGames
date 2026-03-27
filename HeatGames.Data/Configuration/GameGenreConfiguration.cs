using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeatGames.Data.Configuration
{
    public class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
    {
        public void Configure(EntityTypeBuilder<GameGenre> builder)
        {
            // 1. Дефинираме съставен първичен ключ (Composite Primary Key)
            // Тъй като таблицата е свързваща, нейният ключ е комбинация от GameId и GenreId
            builder.HasKey(gg => new { gg.GameId, gg.GenreId });

            // 2. Връзка: Една GameGenre запис сочи към ЕДНА Game, 
            // която има МНОГО GameGenres
            builder.HasOne(gg => gg.Game)
                   .WithMany(g => g.GameGenres)
                   .HasForeignKey(gg => gg.GameId)
                   .OnDelete(DeleteBehavior.Cascade); // Ако изтрием игра, трием и жанровете й

            // 3. Връзка: Един GameGenre запис сочи към ЕДИН Genre, 
            // който има МНОГО GameGenres
            builder.HasOne(gg => gg.Genre)
                   .WithMany(g => g.GameGenres)
                   .HasForeignKey(gg => gg.GenreId)
                   .OnDelete(DeleteBehavior.Cascade);

            var actionId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var rpgId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var shooterId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var strategyId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var adventureId = Guid.Parse("55555555-5555-5555-5555-555555555555");

            builder.HasData(
                new GameGenre { GameId = Guid.Parse("00000001-0000-0000-0000-000000000000"), GenreId = actionId },     // RDR 2
                new GameGenre { GameId = Guid.Parse("00000002-0000-0000-0000-000000000000"), GenreId = actionId },     // GTA V
                new GameGenre { GameId = Guid.Parse("00000003-0000-0000-0000-000000000000"), GenreId = rpgId },        // Cyberpunk
                new GameGenre { GameId = Guid.Parse("00000004-0000-0000-0000-000000000000"), GenreId = rpgId },        // Witcher 3
                new GameGenre { GameId = Guid.Parse("00000005-0000-0000-0000-000000000000"), GenreId = shooterId },    // CS 2
                new GameGenre { GameId = Guid.Parse("00000006-0000-0000-0000-000000000000"), GenreId = strategyId },   // Dota 2
                new GameGenre { GameId = Guid.Parse("00000007-0000-0000-0000-000000000000"), GenreId = adventureId },  // Portal 2
                new GameGenre { GameId = Guid.Parse("00000008-0000-0000-0000-000000000000"), GenreId = shooterId },    // Apex
                new GameGenre { GameId = Guid.Parse("00000009-0000-0000-0000-000000000000"), GenreId = actionId },     // Jedi Survivor
                new GameGenre { GameId = Guid.Parse("0000000a-0000-0000-0000-000000000000"), GenreId = adventureId },  // It Takes Two
                new GameGenre { GameId = Guid.Parse("0000000b-0000-0000-0000-000000000000"), GenreId = rpgId },        // Baldur's Gate 3
                new GameGenre { GameId = Guid.Parse("0000000c-0000-0000-0000-000000000000"), GenreId = rpgId },        // Elden Ring
                new GameGenre { GameId = Guid.Parse("0000000d-0000-0000-0000-000000000000"), GenreId = actionId },     // Sekiro
                new GameGenre { GameId = Guid.Parse("0000000e-0000-0000-0000-000000000000"), GenreId = rpgId },        // AC Valhalla
                new GameGenre { GameId = Guid.Parse("0000000f-0000-0000-0000-000000000000"), GenreId = shooterId }     // Rainbow 6
            );

        }
    }
}