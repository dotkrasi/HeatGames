using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HeatGames.Data.Configurations
{
    public class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
    {
        public void Configure(EntityTypeBuilder<GameGenre> builder)
        {
            builder.HasKey(gg => new { gg.GameId, gg.GenreId });

            var actionId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var rpgId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var shooterId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var strategyId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var advId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var indieId = Guid.Parse("66666666-6666-6666-6666-666666666666"); // Твоят инди жанр

            builder.HasData(
                // 1-15
                new GameGenre { GameId = Guid.Parse("00000001-0000-0000-0000-000000000000"), GenreId = actionId }, new GameGenre { GameId = Guid.Parse("00000001-0000-0000-0000-000000000000"), GenreId = advId },
                new GameGenre { GameId = Guid.Parse("00000002-0000-0000-0000-000000000000"), GenreId = actionId },
                new GameGenre { GameId = Guid.Parse("00000003-0000-0000-0000-000000000000"), GenreId = actionId }, new GameGenre { GameId = Guid.Parse("00000003-0000-0000-0000-000000000000"), GenreId = rpgId },
                new GameGenre { GameId = Guid.Parse("00000004-0000-0000-0000-000000000000"), GenreId = rpgId }, new GameGenre { GameId = Guid.Parse("00000004-0000-0000-0000-000000000000"), GenreId = advId },
                new GameGenre { GameId = Guid.Parse("00000005-0000-0000-0000-000000000000"), GenreId = shooterId },
                new GameGenre { GameId = Guid.Parse("00000006-0000-0000-0000-000000000000"), GenreId = strategyId }, new GameGenre { GameId = Guid.Parse("00000006-0000-0000-0000-000000000000"), GenreId = actionId },
                new GameGenre { GameId = Guid.Parse("00000007-0000-0000-0000-000000000000"), GenreId = advId },
                new GameGenre { GameId = Guid.Parse("00000008-0000-0000-0000-000000000000"), GenreId = shooterId },
                new GameGenre { GameId = Guid.Parse("00000009-0000-0000-0000-000000000000"), GenreId = actionId }, new GameGenre { GameId = Guid.Parse("00000009-0000-0000-0000-000000000000"), GenreId = advId },
                new GameGenre { GameId = Guid.Parse("0000000a-0000-0000-0000-000000000000"), GenreId = advId }, new GameGenre { GameId = Guid.Parse("0000000a-0000-0000-0000-000000000000"), GenreId = actionId },
                new GameGenre { GameId = Guid.Parse("0000000b-0000-0000-0000-000000000000"), GenreId = rpgId }, new GameGenre { GameId = Guid.Parse("0000000b-0000-0000-0000-000000000000"), GenreId = strategyId },
                new GameGenre { GameId = Guid.Parse("0000000c-0000-0000-0000-000000000000"), GenreId = rpgId }, new GameGenre { GameId = Guid.Parse("0000000c-0000-0000-0000-000000000000"), GenreId = actionId },
                new GameGenre { GameId = Guid.Parse("0000000d-0000-0000-0000-000000000000"), GenreId = actionId }, new GameGenre { GameId = Guid.Parse("0000000d-0000-0000-0000-000000000000"), GenreId = advId },
                new GameGenre { GameId = Guid.Parse("0000000e-0000-0000-0000-000000000000"), GenreId = actionId }, new GameGenre { GameId = Guid.Parse("0000000e-0000-0000-0000-000000000000"), GenreId = rpgId },
                new GameGenre { GameId = Guid.Parse("0000000f-0000-0000-0000-000000000000"), GenreId = shooterId }, new GameGenre { GameId = Guid.Parse("0000000f-0000-0000-0000-000000000000"), GenreId = strategyId },
                new GameGenre { GameId = Guid.Parse("00000010-0000-0000-0000-000000000000"), GenreId = strategyId },
                new GameGenre { GameId = Guid.Parse("00000011-0000-0000-0000-000000000000"), GenreId = actionId },
                new GameGenre { GameId = Guid.Parse("00000012-0000-0000-0000-000000000000"), GenreId = rpgId }, new GameGenre { GameId = Guid.Parse("00000012-0000-0000-0000-000000000000"), GenreId = shooterId }, // Mass Effect
                new GameGenre { GameId = Guid.Parse("00000013-0000-0000-0000-000000000000"), GenreId = rpgId }, new GameGenre { GameId = Guid.Parse("00000013-0000-0000-0000-000000000000"), GenreId = actionId }, // AC Odyssey
                new GameGenre { GameId = Guid.Parse("00000014-0000-0000-0000-000000000000"), GenreId = shooterId }, new GameGenre { GameId = Guid.Parse("00000014-0000-0000-0000-000000000000"), GenreId = actionId }, // Far Cry 6
                new GameGenre { GameId = Guid.Parse("00000015-0000-0000-0000-000000000000"), GenreId = actionId }, new GameGenre { GameId = Guid.Parse("00000015-0000-0000-0000-000000000000"), GenreId = advId }, // Watch Dogs
                new GameGenre { GameId = Guid.Parse("00000016-0000-0000-0000-000000000000"), GenreId = shooterId },
                new GameGenre { GameId = Guid.Parse("00000017-0000-0000-0000-000000000000"), GenreId = shooterId },
                new GameGenre { GameId = Guid.Parse("00000018-0000-0000-0000-000000000000"), GenreId = rpgId }, new GameGenre { GameId = Guid.Parse("00000018-0000-0000-0000-000000000000"), GenreId = actionId }, // Dark Souls 3
                new GameGenre { GameId = Guid.Parse("00000019-0000-0000-0000-000000000000"), GenreId = rpgId }, new GameGenre { GameId = Guid.Parse("00000019-0000-0000-0000-000000000000"), GenreId = actionId }, // Bloodborne
                new GameGenre { GameId = Guid.Parse("0000001a-0000-0000-0000-000000000000"), GenreId = rpgId }, new GameGenre { GameId = Guid.Parse("0000001a-0000-0000-0000-000000000000"), GenreId = strategyId }, // DOS 2
                new GameGenre { GameId = Guid.Parse("0000001b-0000-0000-0000-000000000000"), GenreId = shooterId }, new GameGenre { GameId = Guid.Parse("0000001b-0000-0000-0000-000000000000"), GenreId = actionId }, // Max Payne 3
                new GameGenre { GameId = Guid.Parse("0000001c-0000-0000-0000-000000000000"), GenreId = advId }, new GameGenre { GameId = Guid.Parse("0000001c-0000-0000-0000-000000000000"), GenreId = actionId }, // Bully
                new GameGenre { GameId = Guid.Parse("0000001d-0000-0000-0000-000000000000"), GenreId = rpgId }, new GameGenre { GameId = Guid.Parse("0000001d-0000-0000-0000-000000000000"), GenreId = actionId }, // CP Phantom Liberty
                new GameGenre { GameId = Guid.Parse("0000001e-0000-0000-0000-000000000000"), GenreId = indieId }, new GameGenre { GameId = Guid.Parse("0000001e-0000-0000-0000-000000000000"), GenreId = advId }, // Hollow Knight
                new GameGenre { GameId = Guid.Parse("0000001f-0000-0000-0000-000000000000"), GenreId = indieId }, new GameGenre { GameId = Guid.Parse("0000001f-0000-0000-0000-000000000000"), GenreId = rpgId }, // Stardew Valley
                new GameGenre { GameId = Guid.Parse("00000020-0000-0000-0000-000000000000"), GenreId = indieId }, new GameGenre { GameId = Guid.Parse("00000020-0000-0000-0000-000000000000"), GenreId = actionId } // Hades
            );
        }
    }
}