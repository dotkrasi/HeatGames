using HeatGames.Data;
using HeatGamesCore.Services.Interfaces;
using HeatGames.Core.DTOs;
using Microsoft.EntityFrameworkCore;
using HeatGames.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGames.Core.Services
{
    public class GameService : IGameService
    {
        private readonly HeatGamesDbContext _context;

        public GameService(HeatGamesDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<GameDto> Games, int TotalCount)> GetAllGamesAsync(
            string? searchQuery = null,
            string? genre = null,
            Guid? developerId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int page = 1,
            int pageSize = 16)
        {
            var query = _context.Games
                .Include(g => g.GameGenres).ThenInclude(gg => gg.Genre)
                .Include(g => g.GamePlatforms).ThenInclude(gp => gp.Platform)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(g => g.Title.ToLower().Contains(searchQuery.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(g => g.GameGenres.Any(gg => gg.Genre.Name == genre));
            }

            if (developerId.HasValue && developerId.Value != Guid.Empty)
            {
                query = query.Where(g => g.DeveloperId == developerId.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(g => g.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(g => g.Price <= maxPrice.Value);
            }

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
                    DeveloperName = g.Developer.Name,
                    Platforms = g.GamePlatforms.Select(p => p.Platform.Name).ToList(),
                    Genres = g.GameGenres.Select(gg => gg.Genre.Name).ToList()
                })
                .ToListAsync();

            return (games, totalCount);
        }

        public async Task<GameDto?> GetGameByIdAsync(Guid id)
        {
            var game = await _context.Games
                .Include(g => g.Developer)
                .Include(g => g.GameGenres).ThenInclude(gg => gg.Genre)
                .Include(g => g.GamePlatforms).ThenInclude(gp => gp.Platform)
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
                DeveloperName = game.Developer?.Name,
                Genres = game.GameGenres.Select(gg => gg.Genre.Name).ToList(),
                Platforms = game.GamePlatforms.Select(gp => gp.Platform.Name).ToList(),

                SelectedGenreIds = game.GameGenres.Select(gg => gg.GenreId).ToList(),
                SelectedPlatformIds = game.GamePlatforms.Select(gp => gp.PlatformId).ToList()
            };
        }
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

            if (model.SelectedPlatformIds != null)
            {
                foreach (var pId in model.SelectedPlatformIds)
                {
                    await _context.GamePlatforms.AddAsync(new GamePlatform { GameId = game.Id, PlatformId = pId });
                }
            }

            if (model.SelectedGenreIds != null)
            {
                foreach (var gId in model.SelectedGenreIds)
                {
                    await _context.GameGenres.AddAsync(new GameGenre { GameId = game.Id, GenreId = gId });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateGameAsync(GameDto model)
        {
            var game = await _context.Games
                .Include(g => g.GamePlatforms)
                .Include(g => g.GameGenres)
                .FirstOrDefaultAsync(g => g.Id == model.Id);

            if (game == null) return false;

            game.Title = model.Title;
            game.Description = model.Description;
            game.Price = model.Price;
            game.ReleaseDate = model.ReleaseDate;
            game.CoverImageUrl = model.CoverImageUrl;
            game.DeveloperId = model.DeveloperId;

            _context.GamePlatforms.RemoveRange(game.GamePlatforms);
            if (model.SelectedPlatformIds != null)
            {
                foreach (var pId in model.SelectedPlatformIds)
                {
                    _context.GamePlatforms.Add(new GamePlatform { GameId = game.Id, PlatformId = pId });
                }
            }

            _context.GameGenres.RemoveRange(game.GameGenres);
            if (model.SelectedGenreIds != null)
            {
                foreach (var gId in model.SelectedGenreIds)
                {
                    _context.GameGenres.Add(new GameGenre { GameId = game.Id, GenreId = gId });
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
                var libraryItems = _context.LibraryItems.Where(l => l.GameId == id);
                _context.LibraryItems.RemoveRange(libraryItems);

                var gameGenres = _context.GameGenres.Where(gg => gg.GameId == id);
                _context.GameGenres.RemoveRange(gameGenres);

                var reviews = _context.Reviews.Where(r => r.GameId == id);
                _context.Reviews.RemoveRange(reviews);

                var wishlistItems = _context.Wishlists.Where(w => w.GameId == id);
                _context.Wishlists.RemoveRange(wishlistItems);

                var orderItems = _context.OrderItems.Where(o => o.GameId == id);
                _context.OrderItems.RemoveRange(orderItems);

                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }
    }
}