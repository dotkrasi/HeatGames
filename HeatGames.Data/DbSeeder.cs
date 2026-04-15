using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGames.Data
{
    public static class DbSeeder
    {
        public static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<HeatGamesDbContext>();

            // 1. СИЙДВАНЕ НА ЖАНРОВЕ
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

            // 2. СИЙДВАНЕ НА СТУДИА
            if (!await context.Developers.AnyAsync())
            {
                var developers = new List<Developer>
                {
                    new Developer { Name = "Rockstar Games", Website = "rockstargames.com" },
                    new Developer { Name = "Valve", Website = "valvesoftware.com" },
                    new Developer { Name = "CD Projekt RED", Website = "cdprojektred.com" },
                    new Developer { Name = "Electronic Arts", Website = "ea.com" },
                    new Developer { Name = "Ubisoft", Website = "ubisoft.com" },
                    new Developer { Name = "Larian Studios", Website = "larian.com" },
                    new Developer { Name = "FromSoftware", Website = "fromsoftware.jp" }
                };
                await context.Developers.AddRangeAsync(developers);
                await context.SaveChangesAsync();
            }

            // 3. СИЙДВАНЕ НА ПЛАТФОРМИ
            if (!await context.Platforms.AnyAsync())
            {
                var platforms = new List<Platform>
                {
                    new Platform { Name = "PC (Windows)" },
                    new Platform { Name = "PlayStation 5" },
                    new Platform { Name = "Xbox Series X/S" },
                    new Platform { Name = "Nintendo Switch" }
                };
                await context.Platforms.AddRangeAsync(platforms);
                await context.SaveChangesAsync();
            }

            // 4. СИЙДВАНЕ НА ТВОИТЕ 15 ИГРИ
            if (!await context.Games.AnyAsync())
            {
                var rockstarId = (await context.Developers.FirstAsync(d => d.Name == "Rockstar Games")).Id;
                var valveId = (await context.Developers.FirstAsync(d => d.Name == "Valve")).Id;
                var cdprId = (await context.Developers.FirstAsync(d => d.Name == "CD Projekt RED")).Id;
                var eaId = (await context.Developers.FirstAsync(d => d.Name == "Electronic Arts")).Id;
                var ubisoftId = (await context.Developers.FirstAsync(d => d.Name == "Ubisoft")).Id;
                var larianId = (await context.Developers.FirstAsync(d => d.Name == "Larian Studios")).Id;
                var fromSoftId = (await context.Developers.FirstAsync(d => d.Name == "FromSoftware")).Id;

                var games = new List<Game>
                {
                    new Game { Title = "Red Dead Redemption 2", Price = 119.99m, ReleaseDate = new DateTime(2019, 12, 5), Description = "Епична история за живота в безмилостната сърцевина на Америка.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1174180/capsule_616x353.jpg", DeveloperId = rockstarId },
                    new Game { Title = "Grand Theft Auto V", Price = 59.99m, ReleaseDate = new DateTime(2015, 4, 14), Description = "Когато млад уличен измамник и психопат се забъркват с подземния свят...", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/271590/capsule_616x353.jpg", DeveloperId = rockstarId },
                    new Game { Title = "Cyberpunk 2077", Price = 119.00m, ReleaseDate = new DateTime(2020, 12, 10), Description = "Екшън-приключенска ролева игра с отворен свят, развиваща се в Найт Сити.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1091500/capsule_616x353.jpg", DeveloperId = cdprId },
                    new Game { Title = "The Witcher 3: Wild Hunt", Price = 59.99m, ReleaseDate = new DateTime(2015, 5, 18), Description = "Вие сте Гералт от Ривия, наемен убиец на чудовища.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/292030/capsule_616x353.jpg", DeveloperId = cdprId },
                    new Game { Title = "Counter-Strike 2", Price = 0.00m, ReleaseDate = new DateTime(2023, 9, 27), Description = "Следващата ера на Counter-Strike е тук.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/730/capsule_616x353.jpg", DeveloperId = valveId },
                    new Game { Title = "Dota 2", Price = 0.00m, ReleaseDate = new DateTime(2013, 7, 9), Description = "Всеки ден милиони играчи по света влизат в битка като един от над сто героя.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/570/capsule_616x353.jpg", DeveloperId = valveId },
                    new Game { Title = "Portal 2", Price = 19.50m, ReleaseDate = new DateTime(2011, 4, 19), Description = "Иновативен геймплей, история и музика с портали.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/620/capsule_616x353.jpg", DeveloperId = valveId },
                    new Game { Title = "Apex Legends", Price = 0.00m, ReleaseDate = new DateTime(2020, 11, 4), Description = "Покорете с характер в този безплатен Hero Shooter.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1172470/capsule_616x353.jpg", DeveloperId = eaId },
                    new Game { Title = "Star Wars Jedi: Survivor", Price = 139.99m, ReleaseDate = new DateTime(2023, 4, 28), Description = "Историята на Кал Кестис продължава.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1774580/capsule_616x353.jpg", DeveloperId = eaId },
                    new Game { Title = "It Takes Two", Price = 79.99m, ReleaseDate = new DateTime(2021, 3, 26), Description = "Впуснете се в най-лудото пътешествие в живота си.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1426210/capsule_616x353.jpg", DeveloperId = eaId },
                    new Game { Title = "Baldur's Gate 3", Price = 119.99m, ReleaseDate = new DateTime(2023, 8, 3), Description = "Съберете отряда си и се завърнете във Forgotten Realms.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1086940/capsule_616x353.jpg", DeveloperId = larianId },
                    new Game { Title = "Elden Ring", Price = 119.00m, ReleaseDate = new DateTime(2022, 2, 24), Description = "НОВАТА ФЕНТЪЗИ ЕКШЪН RPG ИГРА. Издигнете се и станете Elden Lord.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/1245620/capsule_616x353.jpg", DeveloperId = fromSoftId },
                    new Game { Title = "Sekiro: Shadows Die Twice", Price = 119.00m, ReleaseDate = new DateTime(2019, 3, 21), Description = "Влезте в ролята на 'едноръкия вълк', опозорен и обезобразен воин.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/814380/capsule_616x353.jpg", DeveloperId = fromSoftId },
                    new Game { Title = "Assassin's Creed Valhalla", Price = 119.99m, ReleaseDate = new DateTime(2020, 11, 10), Description = "Станете Ейвор, легендарен викингски рейдер.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/2208920/capsule_616x353.jpg", DeveloperId = ubisoftId },
                    new Game { Title = "Rainbow Six Siege", Price = 39.99m, ReleaseDate = new DateTime(2015, 12, 1), Description = "Включете се в напрегнати, близки битки, тактика и отборна игра.", CoverImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/359550/capsule_616x353.jpg", DeveloperId = ubisoftId }
                };

                await context.Games.AddRangeAsync(games);
                await context.SaveChangesAsync();
            }

            // 5. ВРЪЗВАНЕ НА ПЛАТФОРМИ И ЖАНРОВЕ КЪМ ВСИЧКИ ИГРИ
            if (!await context.GamePlatforms.AnyAsync() && await context.Games.AnyAsync())
            {
                var pcId = (await context.Platforms.FirstAsync(p => p.Name.Contains("PC"))).Id;
                var ps5Id = (await context.Platforms.FirstAsync(p => p.Name.Contains("PlayStation"))).Id;
                var xboxId = (await context.Platforms.FirstAsync(p => p.Name.Contains("Xbox"))).Id;
                var switchId = (await context.Platforms.FirstAsync(p => p.Name.Contains("Nintendo"))).Id;

                var actionId = (await context.Genres.FirstAsync(g => g.Name.Contains("Екшън"))).Id;
                var rpgId = (await context.Genres.FirstAsync(g => g.Name.Contains("RPG"))).Id;
                var shooterId = (await context.Genres.FirstAsync(g => g.Name.Contains("Шутър"))).Id;
                var strategyId = (await context.Genres.FirstAsync(g => g.Name.Contains("Стратегия"))).Id;
                var advId = (await context.Genres.FirstAsync(g => g.Name.Contains("Приключенски"))).Id;

                var games = await context.Games.ToListAsync();

                var rdr2 = games.First(g => g.Title.Contains("Red Dead Redemption"));
                var gta5 = games.First(g => g.Title.Contains("Grand Theft Auto"));
                var cyberpunk = games.First(g => g.Title.Contains("Cyberpunk"));
                var witcher = games.First(g => g.Title.Contains("Witcher"));
                var cs2 = games.First(g => g.Title.Contains("Counter-Strike"));
                var dota2 = games.First(g => g.Title.Contains("Dota"));
                var portal2 = games.First(g => g.Title.Contains("Portal"));
                var apex = games.First(g => g.Title.Contains("Apex"));
                var jedi = games.First(g => g.Title.Contains("Jedi"));
                var itTakesTwo = games.First(g => g.Title.Contains("It Takes Two"));
                var bg3 = games.First(g => g.Title.Contains("Baldur's Gate"));
                var elden = games.First(g => g.Title.Contains("Elden"));
                var sekiro = games.First(g => g.Title.Contains("Sekiro"));
                var acValhalla = games.First(g => g.Title.Contains("Valhalla"));
                var r6 = games.First(g => g.Title.Contains("Rainbow Six"));

                var gamePlatforms = new List<GamePlatform>
                {
                    new GamePlatform { GameId = rdr2.Id, PlatformId = pcId }, new GamePlatform { GameId = rdr2.Id, PlatformId = ps5Id }, new GamePlatform { GameId = rdr2.Id, PlatformId = xboxId },
                    new GamePlatform { GameId = gta5.Id, PlatformId = pcId }, new GamePlatform { GameId = gta5.Id, PlatformId = ps5Id }, new GamePlatform { GameId = gta5.Id, PlatformId = xboxId },
                    new GamePlatform { GameId = cyberpunk.Id, PlatformId = pcId }, new GamePlatform { GameId = cyberpunk.Id, PlatformId = ps5Id }, new GamePlatform { GameId = cyberpunk.Id, PlatformId = xboxId },
                    new GamePlatform { GameId = witcher.Id, PlatformId = pcId }, new GamePlatform { GameId = witcher.Id, PlatformId = ps5Id }, new GamePlatform { GameId = witcher.Id, PlatformId = xboxId }, new GamePlatform { GameId = witcher.Id, PlatformId = switchId },
                    new GamePlatform { GameId = cs2.Id, PlatformId = pcId },
                    new GamePlatform { GameId = dota2.Id, PlatformId = pcId },
                    new GamePlatform { GameId = portal2.Id, PlatformId = pcId }, new GamePlatform { GameId = portal2.Id, PlatformId = switchId },
                    new GamePlatform { GameId = apex.Id, PlatformId = pcId }, new GamePlatform { GameId = apex.Id, PlatformId = ps5Id }, new GamePlatform { GameId = apex.Id, PlatformId = xboxId }, new GamePlatform { GameId = apex.Id, PlatformId = switchId },
                    new GamePlatform { GameId = jedi.Id, PlatformId = pcId }, new GamePlatform { GameId = jedi.Id, PlatformId = ps5Id }, new GamePlatform { GameId = jedi.Id, PlatformId = xboxId },
                    new GamePlatform { GameId = itTakesTwo.Id, PlatformId = pcId }, new GamePlatform { GameId = itTakesTwo.Id, PlatformId = ps5Id }, new GamePlatform { GameId = itTakesTwo.Id, PlatformId = xboxId }, new GamePlatform { GameId = itTakesTwo.Id, PlatformId = switchId },
                    new GamePlatform { GameId = bg3.Id, PlatformId = pcId }, new GamePlatform { GameId = bg3.Id, PlatformId = ps5Id },
                    new GamePlatform { GameId = elden.Id, PlatformId = pcId }, new GamePlatform { GameId = elden.Id, PlatformId = ps5Id }, new GamePlatform { GameId = elden.Id, PlatformId = xboxId },
                    new GamePlatform { GameId = sekiro.Id, PlatformId = pcId }, new GamePlatform { GameId = sekiro.Id, PlatformId = ps5Id }, new GamePlatform { GameId = sekiro.Id, PlatformId = xboxId },
                    new GamePlatform { GameId = acValhalla.Id, PlatformId = pcId }, new GamePlatform { GameId = acValhalla.Id, PlatformId = ps5Id }, new GamePlatform { GameId = acValhalla.Id, PlatformId = xboxId },
                    new GamePlatform { GameId = r6.Id, PlatformId = pcId }, new GamePlatform { GameId = r6.Id, PlatformId = ps5Id }, new GamePlatform { GameId = r6.Id, PlatformId = xboxId }
                };

                var gameGenres = new List<GameGenre>
                {
                    new GameGenre { GameId = rdr2.Id, GenreId = actionId }, new GameGenre { GameId = rdr2.Id, GenreId = advId },
                    new GameGenre { GameId = gta5.Id, GenreId = actionId },
                    new GameGenre { GameId = cyberpunk.Id, GenreId = actionId }, new GameGenre { GameId = cyberpunk.Id, GenreId = rpgId },
                    new GameGenre { GameId = witcher.Id, GenreId = rpgId }, new GameGenre { GameId = witcher.Id, GenreId = advId },
                    new GameGenre { GameId = cs2.Id, GenreId = shooterId },
                    new GameGenre { GameId = dota2.Id, GenreId = actionId }, new GameGenre { GameId = dota2.Id, GenreId = strategyId },
                    new GameGenre { GameId = portal2.Id, GenreId = advId },
                    new GameGenre { GameId = apex.Id, GenreId = shooterId },
                    new GameGenre { GameId = jedi.Id, GenreId = actionId }, new GameGenre { GameId = jedi.Id, GenreId = advId },
                    new GameGenre { GameId = itTakesTwo.Id, GenreId = actionId }, new GameGenre { GameId = itTakesTwo.Id, GenreId = advId },
                    new GameGenre { GameId = bg3.Id, GenreId = rpgId }, new GameGenre { GameId = bg3.Id, GenreId = strategyId },
                    new GameGenre { GameId = elden.Id, GenreId = actionId }, new GameGenre { GameId = elden.Id, GenreId = rpgId },
                    new GameGenre { GameId = sekiro.Id, GenreId = actionId }, new GameGenre { GameId = sekiro.Id, GenreId = advId },
                    new GameGenre { GameId = acValhalla.Id, GenreId = actionId }, new GameGenre { GameId = acValhalla.Id, GenreId = rpgId },
                    new GameGenre { GameId = r6.Id, GenreId = shooterId }, new GameGenre { GameId = r6.Id, GenreId = strategyId }
                };

                await context.GamePlatforms.AddRangeAsync(gamePlatforms);
                await context.GameGenres.AddRangeAsync(gameGenres);
                await context.SaveChangesAsync();
            }
        }
    }
}