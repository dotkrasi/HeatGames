using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HeatGames.Data.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            var rockstar = Guid.Parse("aaaa0000-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var valve = Guid.Parse("bbbb0000-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var cdpr = Guid.Parse("cccc0000-cccc-cccc-cccc-cccccccccccc");
            var ea = Guid.Parse("dddd0000-dddd-dddd-dddd-dddddddddddd");
            var ubisoft = Guid.Parse("eeee0000-eeee-eeee-eeee-eeeeeeeeeeee");
            var larian = Guid.Parse("ffff0000-ffff-ffff-ffff-ffffffffffff");
            var fromSoftware = Guid.Parse("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            builder.HasData(
                new Game { Id = Guid.Parse("00000001-0000-0000-0000-000000000000"), Title = "Red Dead Redemption 2", Price = 119.99m, ReleaseDate = new DateTime(2019, 12, 5), Description = "Епична история за живота в безмилостната сърцевина на Америка.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1174180/capsule_616x353.jpg", DeveloperId = rockstar },
                new Game { Id = Guid.Parse("00000002-0000-0000-0000-000000000000"), Title = "Grand Theft Auto V", Price = 59.99m, ReleaseDate = new DateTime(2015, 4, 14), Description = "Когато млад уличен измамник и психопат се забъркват с подземния свят...", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/271590/capsule_616x353.jpg", DeveloperId = rockstar },
                new Game { Id = Guid.Parse("00000003-0000-0000-0000-000000000000"), Title = "Cyberpunk 2077", Price = 119.00m, ReleaseDate = new DateTime(2020, 12, 10), Description = "Екшън-приключенска ролева игра с отворен свят, развиваща се в Найт Сити.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1091500/capsule_616x353.jpg", DeveloperId = cdpr },
                new Game { Id = Guid.Parse("00000004-0000-0000-0000-000000000000"), Title = "The Witcher 3: Wild Hunt", Price = 59.99m, ReleaseDate = new DateTime(2015, 5, 18), Description = "Вие сте Гералт от Ривия, наемен убиец на чудовища.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/292030/capsule_616x353.jpg", DeveloperId = cdpr },
                new Game { Id = Guid.Parse("00000005-0000-0000-0000-000000000000"), Title = "Counter-Strike 2", Price = 0.00m, ReleaseDate = new DateTime(2023, 9, 27), Description = "Следващата ера на Counter-Strike е тук.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/730/capsule_616x353.jpg", DeveloperId = valve },
                new Game { Id = Guid.Parse("00000006-0000-0000-0000-000000000000"), Title = "Dota 2", Price = 0.00m, ReleaseDate = new DateTime(2013, 7, 9), Description = "Всеки ден милиони играчи по света влизат в битка като един от над сто героя.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/570/capsule_616x353.jpg", DeveloperId = valve },
                new Game { Id = Guid.Parse("00000007-0000-0000-0000-000000000000"), Title = "Portal 2", Price = 19.50m, ReleaseDate = new DateTime(2011, 4, 19), Description = "Иновативен геймплей, история и музика с портали.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/620/capsule_616x353.jpg", DeveloperId = valve },
                new Game { Id = Guid.Parse("00000008-0000-0000-0000-000000000000"), Title = "Apex Legends", Price = 0.00m, ReleaseDate = new DateTime(2020, 11, 4), Description = "Покорете с характер в този безплатен Hero Shooter.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1172470/capsule_616x353.jpg", DeveloperId = ea },
                new Game { Id = Guid.Parse("00000009-0000-0000-0000-000000000000"), Title = "Star Wars Jedi: Survivor", Price = 139.99m, ReleaseDate = new DateTime(2023, 4, 28), Description = "Историята на Кал Кестис продължава.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1774580/capsule_616x353.jpg", DeveloperId = ea },
                new Game { Id = Guid.Parse("0000000a-0000-0000-0000-000000000000"), Title = "It Takes Two", Price = 79.99m, ReleaseDate = new DateTime(2021, 3, 26), Description = "Впуснете се в най-лудото пътешествие в живота си.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1426210/capsule_616x353.jpg", DeveloperId = ea },
                new Game { Id = Guid.Parse("0000000b-0000-0000-0000-000000000000"), Title = "Baldur's Gate 3", Price = 119.99m, ReleaseDate = new DateTime(2023, 8, 3), Description = "Съберете отряда си и се завърнете във Forgotten Realms.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1086940/capsule_616x353.jpg", DeveloperId = larian },
                new Game { Id = Guid.Parse("0000000c-0000-0000-0000-000000000000"), Title = "Elden Ring", Price = 119.00m, ReleaseDate = new DateTime(2022, 2, 24), Description = "НОВАТА ФЕНТЪЗИ ЕКШЪН RPG ИГРА. Издигнете се и станете Elden Lord.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1245620/capsule_616x353.jpg", DeveloperId = fromSoftware },
                new Game { Id = Guid.Parse("0000000d-0000-0000-0000-000000000000"), Title = "Sekiro: Shadows Die Twice", Price = 119.00m, ReleaseDate = new DateTime(2019, 3, 21), Description = "Влезте в ролята на 'едноръкия вълк', опозорен и обезобразен воин.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/814380/capsule_616x353.jpg", DeveloperId = fromSoftware },
                new Game { Id = Guid.Parse("0000000e-0000-0000-0000-000000000000"), Title = "Assassin's Creed Valhalla", Price = 119.99m, ReleaseDate = new DateTime(2020, 11, 10), Description = "Станете Ейвор, легендарен викингски рейдер.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/2208920/capsule_616x353.jpg", DeveloperId = ubisoft },
                new Game { Id = Guid.Parse("0000000f-0000-0000-0000-000000000000"), Title = "Rainbow Six Siege", Price = 39.99m, ReleaseDate = new DateTime(2015, 12, 1), Description = "Включете се в напрегнати, близки битки, тактика и отборна игра.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/359550/capsule_616x353.jpg", DeveloperId = ubisoft }
            );
        }
    }
} 