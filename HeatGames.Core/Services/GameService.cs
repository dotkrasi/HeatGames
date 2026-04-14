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

        // 1. Промени заглавието на метода, за да приема page и pageSize
        public async Task<(IEnumerable<GameDto> Games, int TotalCount)> GetAllGamesAsync(
            string? searchQuery = null,
            string? genre = null,
            decimal? maxPrice = null,
            int page = 1,
            int pageSize = 8) // Добавени параметри
        {
            var query = _context.Games
                .Include(g => g.GameGenres)
                .ThenInclude(gg => gg.Genre)
                .AsQueryable();

            // ТВОЯТА ЛОГИКА ЗА ФИЛТРИРАНЕ (остава същата)
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(g => g.Title.Contains(searchQuery));
            }
            // ... останалите ти филтри за жанр и цена ...

            // НОВО: Взимаме общата бройка ПРЕДИ да срежем списъка за страницата
            int totalCount = await query.CountAsync();

            // НОВО: Добавяме Skip и Take към твоята заявка
            var games = await query
                .OrderByDescending(g => g.ReleaseDate) // Добре е да са сортирани
                .Skip((page - 1) * pageSize)           // Прескачаме старите страници
                .Take(pageSize)                        // Взимаме само 8 за текущата
                .Select(g => new GameDto
                {
                    Id = g.Id,
                    Title = g.Title,
                    Price = g.Price,
                    CoverImageUrl = g.CoverImageUrl,
                    // ... твоите останали полета ...
                })
                .ToListAsync();

            return (games, totalCount); // Връщаме и двете
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