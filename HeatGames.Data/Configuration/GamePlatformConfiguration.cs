using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HeatGames.Data.Configurations
{
    public class GamePlatformConfiguration : IEntityTypeConfiguration<GamePlatform>
    {
        public void Configure(EntityTypeBuilder<GamePlatform> builder)
        {
            builder.HasKey(gp => new { gp.GameId, gp.PlatformId });

            var pcId = Guid.Parse("11111111-2222-3333-4444-555555555555");
            var ps5Id = Guid.Parse("22222222-3333-4444-5555-666666666666");
            var xboxId = Guid.Parse("33333333-4444-5555-6666-777777777777");
            var switchId = Guid.Parse("44444444-5555-6666-7777-888888888888");

            builder.HasData(
                // 1-15
                new GamePlatform { GameId = Guid.Parse("00000001-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000001-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("00000001-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("00000002-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000002-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("00000002-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("00000003-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000003-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("00000003-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("00000004-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000004-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("00000004-0000-0000-0000-000000000000"), PlatformId = xboxId }, new GamePlatform { GameId = Guid.Parse("00000004-0000-0000-0000-000000000000"), PlatformId = switchId },
                new GamePlatform { GameId = Guid.Parse("00000005-0000-0000-0000-000000000000"), PlatformId = pcId },
                new GamePlatform { GameId = Guid.Parse("00000006-0000-0000-0000-000000000000"), PlatformId = pcId },
                new GamePlatform { GameId = Guid.Parse("00000007-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000007-0000-0000-0000-000000000000"), PlatformId = switchId },
                new GamePlatform { GameId = Guid.Parse("00000008-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000008-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("00000008-0000-0000-0000-000000000000"), PlatformId = xboxId }, new GamePlatform { GameId = Guid.Parse("00000008-0000-0000-0000-000000000000"), PlatformId = switchId },
                new GamePlatform { GameId = Guid.Parse("00000009-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000009-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("00000009-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("0000000a-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("0000000a-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("0000000a-0000-0000-0000-000000000000"), PlatformId = xboxId }, new GamePlatform { GameId = Guid.Parse("0000000a-0000-0000-0000-000000000000"), PlatformId = switchId },
                new GamePlatform { GameId = Guid.Parse("0000000b-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("0000000b-0000-0000-0000-000000000000"), PlatformId = ps5Id },
                new GamePlatform { GameId = Guid.Parse("0000000c-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("0000000c-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("0000000c-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("0000000d-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("0000000d-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("0000000d-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("0000000e-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("0000000e-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("0000000e-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("0000000f-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("0000000f-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("0000000f-0000-0000-0000-000000000000"), PlatformId = xboxId },

                // 16-32
                new GamePlatform { GameId = Guid.Parse("00000010-0000-0000-0000-000000000000"), PlatformId = pcId },
                new GamePlatform { GameId = Guid.Parse("00000011-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000011-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("00000011-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("00000012-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000012-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("00000012-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("00000013-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000013-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("00000013-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("00000014-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000014-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("00000014-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("00000015-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000015-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("00000015-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("00000016-0000-0000-0000-000000000000"), PlatformId = pcId },
                new GamePlatform { GameId = Guid.Parse("00000017-0000-0000-0000-000000000000"), PlatformId = pcId },
                new GamePlatform { GameId = Guid.Parse("00000018-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000018-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("00000018-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("00000019-0000-0000-0000-000000000000"), PlatformId = ps5Id }, // Bloodborne е само за PS5
                new GamePlatform { GameId = Guid.Parse("0000001a-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("0000001a-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("0000001a-0000-0000-0000-000000000000"), PlatformId = switchId },
                new GamePlatform { GameId = Guid.Parse("0000001b-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("0000001b-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("0000001c-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("0000001c-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("0000001c-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("0000001d-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("0000001d-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("0000001d-0000-0000-0000-000000000000"), PlatformId = xboxId },
                new GamePlatform { GameId = Guid.Parse("0000001e-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("0000001e-0000-0000-0000-000000000000"), PlatformId = switchId },
                new GamePlatform { GameId = Guid.Parse("0000001f-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("0000001f-0000-0000-0000-000000000000"), PlatformId = switchId },
                new GamePlatform { GameId = Guid.Parse("00000020-0000-0000-0000-000000000000"), PlatformId = pcId }, new GamePlatform { GameId = Guid.Parse("00000020-0000-0000-0000-000000000000"), PlatformId = ps5Id }, new GamePlatform { GameId = Guid.Parse("00000020-0000-0000-0000-000000000000"), PlatformId = switchId }
            );
        }
    }
}