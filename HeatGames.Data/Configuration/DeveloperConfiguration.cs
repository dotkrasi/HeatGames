using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeatGames.Data.Configuration
{
    public class DeveloperConfiguration : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> builder)
        {
            // Налагаме ограниченията на ниво SQL база данни
            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.Website)
                   .HasMaxLength(255);
            builder.HasData(
                new Developer { Id = Guid.Parse("aaaa0000-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Rockstar Games", Website = "rockstargames.com" },
                new Developer { Id = Guid.Parse("bbbb0000-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "Valve", Website = "valvesoftware.com" },
                new Developer { Id = Guid.Parse("cccc0000-cccc-cccc-cccc-cccccccccccc"), Name = "CD Projekt RED", Website = "cdprojektred.com" },
                new Developer { Id = Guid.Parse("dddd0000-dddd-dddd-dddd-dddddddddddd"), Name = "Electronic Arts", Website = "ea.com" },
                new Developer { Id = Guid.Parse("eeee0000-eeee-eeee-eeee-eeeeeeeeeeee"), Name = "Ubisoft", Website = "ubisoft.com" },
                new Developer { Id = Guid.Parse("ffff0000-ffff-ffff-ffff-ffffffffffff"), Name = "Larian Studios", Website = "larian.com" },
                new Developer { Id = Guid.Parse("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "FromSoftware", Website = "fromsoftware.jp" }
            );
        }
    }
}