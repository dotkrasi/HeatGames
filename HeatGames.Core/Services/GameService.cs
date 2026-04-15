using HeatGames.Data;
using HeatGamesCore.Services.Interfaces;
using HeatGames.Core.DTOs;
using Microsoft.EntityFrameworkCore;
using HeatGames.Data.Models;

namespace HeatGames.Core.Services
{
    public class GameService : IGameService
    {
        private readonly HeatGamesDbContext _context;

        public GameService(HeatGamesDbContext context)
        {
            _context = context;
        }

        // 1. В GetAllGamesAsync добавяме Include за платформите
        public async Task<(IEnumerable<GameDto> Games, int TotalCount)> GetAllGamesAsync(
            string? searchQuery = null, string? genre = null, decimal? maxPrice = null, int page = 1, int pageSize = 8)
        {
            var query = _context.Games
                .Include(g => g.GameGenres).ThenInclude(gg => gg.Genre)
                .Include(g => g.GamePlatforms).ThenInclude(gp => gp.Platform) // НОВО
                .AsQueryable();

            // ... филтрирането си остава същото ...

            int totalCount = await query.CountAsync();

            var games = await query
                .OrderByDescending(g => g.ReleaseDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(g => new GameDto
                {
                    Id = g.Id,
                    Title = g.Title,
                    Price = g.Price,
                    CoverImageUrl = g.CoverImageUrl,
                    DeveloperId = g.DeveloperId,
                    // Мапваме имената на платформите за показване в каталога
                    Platforms = g.GamePlatforms.Select(p => p.Platform.Name).ToList()
                })
                .ToListAsync();

            return (games, totalCount);
        }

        // 2. В GetGameByIdAsync също добавяме платформите
        public async Task<GameDto?> GetGameByIdAsync(Guid id)
        {
            var game = await _context.Games
                .Include(g => g.GameGenres) // Зареждаме жанровете
                    .ThenInclude(gg => gg.Genre)
                .Include(g => g.GamePlatforms) // Зареждаме платформите
                    .ThenInclude(gp => gp.Platform)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null) return null;

            return new GameDto
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate,
                CoverImageUrl = game.CoverImageUrl,
                DeveloperId = game.DeveloperId,
                // ТУК ПЪЛНИМ СПИСЪЦИТЕ С ИМЕНА:
                Genres = game.GameGenres.Select(gg => gg.Genre.Name).ToList(),
                Platforms = game.GamePlatforms.Select(gp => gp.Platform.Name).ToList()
            };
        }

        // 3. В CreateGameAsync записваме връзките в GamePlatforms
        public async Task CreateGameAsync(GameDto model)
        {
            var game = new Game
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                ReleaseDate = model.ReleaseDate,
                CoverImageUrl = model.CoverImageUrl,
                DeveloperId = model.DeveloperId
            };

            await _context.Games.AddAsync(game);

            // Добавяме платформите
            foreach (var platformId in model.SelectedPlatformIds)
            {
                await _context.GamePlatforms.AddAsync(new GamePlatform
                {
                    GameId = game.Id,
                    PlatformId = platformId
                });
            }

            await _context.SaveChangesAsync();
        }

        // 4. В UpdateGameAsync изтриваме старите и добавяме новите платформи
        public async Task<bool> UpdateGameAsync(GameDto model)
        {
            var game = await _context.Games
                .Include(g => g.GamePlatforms)
                .Include(g => g.GameGenres) // ВКЛЮЧИ ЖАНРОВЕТЕ
                .FirstOrDefaultAsync(g => g.Id == model.Id);

            if (game == null) return false;

            // Обновяване на основни данни
            game.Title = model.Title;
            game.Description = model.Description;
            game.Price = model.Price;
            game.ReleaseDate = model.ReleaseDate;
            game.CoverImageUrl = model.CoverImageUrl;
            game.DeveloperId = model.DeveloperId;

            // 1. Обновяване на Платформи (както досега)
            _context.GamePlatforms.RemoveRange(game.GamePlatforms);
            if (model.SelectedPlatformIds != null)
            {
                foreach (var pId in model.SelectedPlatformIds)
                {
                    game.GamePlatforms.Add(new GamePlatform { PlatformId = pId });
                }
            }

            // 2. ОБНОВЯВАНЕ НА ЖАНРОВЕ
            _context.GameGenres.RemoveRange(game.GameGenres); // Махаме старите
            if (model.SelectedGenreIds != null)
            {
                foreach (var gId in model.SelectedGenreIds)
                {
                    game.GameGenres.Add(new GameGenre { GenreId = gId });
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteGameAsync(Guid id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game != null)
            {
                // 1. Изтриваме играта от библиотеките на потребителите
                var libraryItems = _context.LibraryItems.Where(l => l.GameId == id);
                _context.LibraryItems.RemoveRange(libraryItems);

                // 2. Изтриваме връзките на играта с жанровете (GameGenres)
                var gameGenres = _context.GameGenres.Where(gg => gg.GameId == id);
                _context.GameGenres.RemoveRange(gameGenres);

                // 3. Изтриваме всички ревюта, написани за тази игра
                var reviews = _context.Reviews.Where(r => r.GameId == id);
                _context.Reviews.RemoveRange(reviews);

                // 4. АКО ИМАШ Wishlist таблица, разкоментирай тези редове:
                var wishlistItems = _context.Wishlists.Where(w => w.GameId == id);
                _context.Wishlists.RemoveRange(wishlistItems);

                // 5.АКО ИМАШ OrderItems(история на поръчките), разкоментирай тези редове:
                var orderItems = _context.OrderItems.Where(o => o.GameId == id);
                _context.OrderItems.RemoveRange(orderItems);

                // НАКРАЯ: След като сме изчистили всички зависимости, спокойно трием самата игра!
                _context.Games.Remove(game);

                // Запазваме всички промени наведнъж
                await _context.SaveChangesAsync();
            }
        }
    }
}