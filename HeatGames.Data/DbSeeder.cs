using HeatGames.Data.Models; // Увери се, че това е твоят namespace за моделите
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGames.Data // Увери се, че това е твоят namespace
{
    public static class DbSeeder
    {
        public static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            // Взимаме връзката с базата данни
            using var context = serviceProvider.GetRequiredService<HeatGamesDbContext>(); // Смени името на DbContext-а, ако твоето е различно!

            // 1. СИЙДВАНЕ НА ЖАНРОВЕ (Ако базата е празна)
            if (!await context.Genres.AnyAsync())
            {
                var genres = new List<Genre>
                {
                    new Genre { Name = "Екшън (Action)" },
                    new Genre { Name = "RPG (Ролеви)" },
                    new Genre { Name = "Шутър (Shooter)" },
                    new Genre { Name = "Стратегия (Strategy)" },
                    new Genre { Name = "Приключенски (Adventure)" }
                };
                await context.Genres.AddRangeAsync(genres);
                await context.SaveChangesAsync();
            }

            // 2. СИЙДВАНЕ НА СТУДИА / DEVELOPERS (Ако базата е празна - ВЕЧЕ БЕЗ FoundedYear!)
            if (!await context.Developers.AnyAsync())
            {
                var developers = new List<Developer>
                {
                    new Developer { Name = "Rockstar Games", Website = "rockstargames.com" },
                    new Developer { Name = "Valve", Website = "valvesoftware.com" },
                    new Developer { Name = "CD Projekt RED", Website = "cdprojektred.com" },
                    new Developer { Name = "Electronic Arts", Website = "ea.com" },
                    new Developer { Name = "Ubisoft", Website = "ubisoft.com" }
                };
                await context.Developers.AddRangeAsync(developers);
                await context.SaveChangesAsync();
            }

            // 3. СИЙДВАНЕ НА ИГРИ (Свързваме ги с жанровете и студиата)
            if (!await context.Games.AnyAsync())
            {
                // Взимаме вече създадените жанрове и студиа, за да им вземем ID-тата
                var actionId = (await context.Genres.FirstAsync(g => g.Name.Contains("Екшън"))).Id;
                var rpgId = (await context.Genres.FirstAsync(g => g.Name.Contains("RPG"))).Id;
                var shooterId = (await context.Genres.FirstAsync(g => g.Name.Contains("Шутър"))).Id;

                var rockstarId = (await context.Developers.FirstAsync(d => d.Name == "Rockstar Games")).Id;
                var valveId = (await context.Developers.FirstAsync(d => d.Name == "Valve")).Id;
                var cdprId = (await context.Developers.FirstAsync(d => d.Name == "CD Projekt RED")).Id;
                var eaId = (await context.Developers.FirstAsync(d => d.Name == "Electronic Arts")).Id;

                var games = new List<Game>
                {
                    new Game { Title = "Red Dead Redemption 2", Price = 119.99m, ReleaseDate = new DateTime(2019, 12, 5), Description = "Епична история за живота в безмилостната сърцевина на Америка.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1174180/capsule_616x353.jpg", DeveloperId = rockstarId },
                    new Game { Title = "Grand Theft Auto V", Price = 59.99m, ReleaseDate = new DateTime(2015, 4, 14), Description = "Когато млад уличен измамник, пенсиониран банков обирджия и ужасяващ психопат се забъркват с подземния свят...", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/271590/capsule_616x353.jpg", DeveloperId = rockstarId },
                    new Game { Title = "Cyberpunk 2077", Price = 119.00m, ReleaseDate = new DateTime(2020, 12, 10), Description = "Екшън-приключенска ролева игра с отворен свят, развиваща се в мегалополиса Найт Сити.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1091500/capsule_616x353.jpg", DeveloperId = cdprId },
                    new Game { Title = "The Witcher 3: Wild Hunt", Price = 59.99m, ReleaseDate = new DateTime(2015, 5, 18), Description = "Вие сте Гералт от Ривия, наемен убиец на чудовища. Пред вас се простира разкъсван от войни континент.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/292030/capsule_616x353.jpg", DeveloperId = cdprId },
                    new Game { Title = "Counter-Strike 2", Price = 0.00m, ReleaseDate = new DateTime(2023, 9, 27), Description = "Повече от две десетилетия Counter-Strike предлага елитно състезателно преживяване.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/730/capsule_616x353.jpg", DeveloperId = valveId },
                    new Game { Title = "Dota 2", Price = 0.00m, ReleaseDate = new DateTime(2013, 7, 9), Description = "Всеки ден милиони играчи по света влизат в битка като един от над сто героя в Dota 2.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/570/capsule_616x353.jpg", DeveloperId = valveId },
                    new Game { Title = "Portal 2", Price = 19.50m, ReleaseDate = new DateTime(2011, 4, 19), Description = "Portal 2 черпи от награждаваната формула на иновативен геймплей, история и музика.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/620/capsule_616x353.jpg", DeveloperId = valveId },
                    new Game { Title = "Apex Legends", Price = 0.00m, ReleaseDate = new DateTime(2020, 11, 4), Description = "Покорете с характер в Apex Legends, безплатен Hero Shooter, където легендарни герои се бият за слава.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1172470/capsule_616x353.jpg", DeveloperId = eaId },
                    new Game { Title = "Half-Life: Alyx", Price = 115.00m, ReleaseDate = new DateTime(2020, 3, 23), Description = "Завръщането на Valve към вселената на Half-Life във виртуална реалност.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/546560/capsule_616x353.jpg", DeveloperId = valveId },
                    new Game { Title = "Star Wars Jedi: Survivor", Price = 139.99m, ReleaseDate = new DateTime(2023, 4, 28), Description = "Историята на Кал Кестис продължава в това галактическо приключение от трето лице.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1774580/capsule_616x353.jpg", DeveloperId = eaId  },
                    new Game { Title = "It Takes Two", Price = 79.99m, ReleaseDate = new DateTime(2021, 3, 26), Description = "Впуснете се в най-лудото пътешествие в живота си в It Takes Two.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1426210/capsule_616x353.jpg", DeveloperId = eaId }
                };

                await context.Games.AddRangeAsync(games);
                await context.SaveChangesAsync();
            }
        }
    }
}