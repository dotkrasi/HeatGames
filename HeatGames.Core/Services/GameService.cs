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

        public async Task<IEnumerable<GameDto>> GetAllGamesAsync()
        {
            // Взимаме игрите от базата и ги "мапваме" към ViewModel
            return await _context.Games
                .Select(g => new GameDto
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    Price = g.Price,
                    ReleaseDate = g.ReleaseDate,
                    CoverImageUrl = g.CoverImageUrl,
                    DeveloperId = g.DeveloperId
                })
                .ToListAsync();
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
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }
    }
}