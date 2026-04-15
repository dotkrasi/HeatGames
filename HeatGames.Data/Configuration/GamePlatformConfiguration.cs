using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HeatGames.Data.Configuration
{
    public class GamePlatformConfiguration : IEntityTypeConfiguration<GamePlatform>
    {
        public void Configure(EntityTypeBuilder<GamePlatform> builder)
        {
            // 1. Дефинираме съставен първичен ключ (GameId + PlatformId)
            builder.HasKey(gp => new { gp.GameId, gp.PlatformId });

            // 2. Връзки (Relations) с каскадно изтриване
            builder.HasOne(gp => gp.Game)
                   .WithMany(g => g.GamePlatforms)
                   .HasForeignKey(gp => gp.GameId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(gp => gp.Platform)
                   .WithMany(p => p.GamePlatforms)
                   .HasForeignKey(gp => gp.PlatformId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 3. Сийдване: Закачаме RDR2, GTA V и Cyberpunk към различни платформи
            var pcId = Guid.Parse("11111111-2222-3333-4444-555555555555");
            var ps5Id = Guid.Parse("22222222-3333-4444-5555-666666666666");
            var xboxId = Guid.Parse("33333333-4444-5555-6666-777777777777");

            builder.HasData(
                new GamePlatform { GameId = Guid.Parse("00000001-0000-0000-0000-000000000000"), PlatformId = pcId },  // RDR 2 -> PC
                new GamePlatform { GameId = Guid.Parse("00000001-0000-0000-0000-000000000000"), PlatformId = ps5Id }, // RDR 2 -> PS5

                new GamePlatform { GameId = Guid.Parse("00000002-0000-0000-0000-000000000000"), PlatformId = pcId },  // GTA V -> PC
                new GamePlatform { GameId = Guid.Parse("00000002-0000-0000-0000-000000000000"), PlatformId = ps5Id }, // GTA V -> PS5
                new GamePlatform { GameId = Guid.Parse("00000002-0000-0000-0000-000000000000"), PlatformId = xboxId },// GTA V -> Xbox

                new GamePlatform { GameId = Guid.Parse("00000003-0000-0000-0000-000000000000"), PlatformId = pcId },  // Cyberpunk -> PC
                new GamePlatform { GameId = Guid.Parse("00000003-0000-0000-0000-000000000000"), PlatformId = ps5Id }  // Cyberpunk -> PS5
            );
        }
    }
}