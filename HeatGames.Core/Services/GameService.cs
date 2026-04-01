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

        public async Task<IEnumerable<GameDto>> GetAllGamesAsync(string? searchQuery = null, string? genre = null, decimal? maxPrice = null)
        {
            // Започваме със заявка, която взима всички игри и техните жанрове
            var query = _context.Games
                .Include(g => g.GameGenres)
                .ThenInclude(gg => gg.Genre)
                .AsQueryable();

            // 1. Филтър по ТЪРСЕНЕ (ако потребителят е написал нещо)
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(g => g.Title.Contains(searchQuery));
            }

            // 2. Филтър по ЖАНР (ако потребителят е кликнал на жанр в сайдбара)
            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(g => g.GameGenres.Any(gg => gg.Genre.Name.Contains(genre)));
            }

            // 3. Филтър по ЦЕНА (ако потребителят е кликнал на цена)
            if (maxPrice.HasValue)
            {
                // Ако цената е 0, значи търсим "Безплатни"
                if (maxPrice.Value == 0)
                {
                    query = query.Where(g => g.Price == 0);
                }
                else
                {
                    query = query.Where(g => g.Price <= maxPrice.Value);
                }
            }

            // Изпълняваме заявката и мапваме към DTO
            var games = await query
                .Select(g => new GameDto
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    Price = g.Price,
                    ReleaseDate = g.ReleaseDate,
                    CoverImageUrl = g.CoverImageUrl,
                    DeveloperId = g.DeveloperId,
                    // (По избор) Взимаме и жанровете като списък от стрингове, ако искаш да ги показваш
                    // Genres = g.GameGenres.Select(gg => gg.Genre.Name).ToList() 
                })
                .ToListAsync();

            return games;
        }

        public async Task<GameDto?> GetGameByIdAsync(Guid id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return null;

            return new GameDto
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate,
                CoverImageUrl = game.CoverImageUrl,
                DeveloperId = game.DeveloperId
            };
        }

        public async Task CreateGameAsync(GameDto model)
        {
            // Създаваме ново Entity от данните на ViewModel-а
            var game = new Game
            {
                Id = model.Id, // Вече е генерирано в контролера
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                ReleaseDate = model.ReleaseDate,
                CoverImageUrl = model.CoverImageUrl,
                DeveloperId = model.DeveloperId
            };

            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateGameAsync(GameDto model)
        {
            var game = await _context.Games.FindAsync(model.Id);
            if (game == null) return false;

            // Обновяваме полетата
            game.Title = model.Title;
            game.Description = model.Description;
            game.Price = model.Price;
            game.ReleaseDate = model.ReleaseDate;
            game.CoverImageUrl = model.CoverImageUrl;
            game.DeveloperId = model.DeveloperId;

            _context.Games.Update(game);
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