using HeatGames.Core.DTOs;
using HeatGames.Core.Services;
using HeatGames.Data;
using HeatGames.Data.Models;
using HeatGames.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGames.Tests.Services
{
    [TestFixture]
    public class GameServiceTests
    {
        private HeatGamesDbContext _context;
        private GameService _gameService;

        [SetUp]
        public void SetUp()
        {
            _context = DbContextHelper.GetInMemoryDbContext();
            
            // Clear seeded data
            _context.Games.RemoveRange(_context.Games);
            _context.Developers.RemoveRange(_context.Developers);
            _context.Genres.RemoveRange(_context.Genres);
            _context.Platforms.RemoveRange(_context.Platforms);
            _context.GameGenres.RemoveRange(_context.GameGenres);
            _context.GamePlatforms.RemoveRange(_context.GamePlatforms);
            _context.LibraryItems.RemoveRange(_context.LibraryItems);
            _context.Reviews.RemoveRange(_context.Reviews);
            _context.Wishlists.RemoveRange(_context.Wishlists);
            _context.OrderItems.RemoveRange(_context.OrderItems);
            
            _context.SaveChanges();
            
            _gameService = new GameService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAllGamesAsync_WithoutFilters_ReturnsAllGames()
        {
            var dev = new Developer { Id = Guid.NewGuid(), Name = "Dev" };
            _context.Developers.Add(dev);
            
            _context.Games.Add(new Game { Id = Guid.NewGuid(), Title = "Game1", Description = "D", DeveloperId = dev.Id, Price = 10 });
            _context.Games.Add(new Game { Id = Guid.NewGuid(), Title = "Game2", Description = "D", DeveloperId = dev.Id, Price = 20 });
            await _context.SaveChangesAsync();

            var result = await _gameService.GetAllGamesAsync();

            Assert.That(result.Games.Count(), Is.EqualTo(2));
            Assert.That(result.TotalCount, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllGamesAsync_WithSearchQuery_ReturnsFilteredGames()
        {
            var dev = new Developer { Id = Guid.NewGuid(), Name = "Dev" };
            _context.Developers.Add(dev);
            
            _context.Games.Add(new Game { Id = Guid.NewGuid(), Title = "Action Game", Description = "D", DeveloperId = dev.Id });
            _context.Games.Add(new Game { Id = Guid.NewGuid(), Title = "RPG Game", Description = "D", DeveloperId = dev.Id });
            await _context.SaveChangesAsync();

            var result = await _gameService.GetAllGamesAsync(searchQuery: "Action");

            Assert.That(result.Games.Count(), Is.EqualTo(1));
            Assert.That(result.Games.First().Title, Is.EqualTo("Action Game"));
        }

        [Test]
        public async Task CreateGameAsync_AddsGameAndRelationships()
        {
            var platformId = Guid.NewGuid();
            var genreId = Guid.NewGuid();
            
            _context.Platforms.Add(new Platform { Id = platformId, Name = "PC" });
            _context.Genres.Add(new Genre { Id = genreId, Name = "RPG" });
            await _context.SaveChangesAsync();

            var dto = new GameDto
            {
                Id = Guid.NewGuid(),
                Title = "New Game",
                Description = "D",
                SelectedPlatformIds = new List<Guid> { platformId },
                SelectedGenreIds = new List<Guid> { genreId }
            };

            await _gameService.CreateGameAsync(dto);

            var gameInDb = _context.Games.FirstOrDefault(g => g.Id == dto.Id);
            Assert.That(gameInDb, Is.Not.Null);
            Assert.That(gameInDb.Title, Is.EqualTo("New Game"));
            Assert.That(_context.GamePlatforms.Any(gp => gp.GameId == dto.Id && gp.PlatformId == platformId), Is.True);
            Assert.That(_context.GameGenres.Any(gg => gg.GameId == dto.Id && gg.GenreId == genreId), Is.True);
        }

        [Test]
        public async Task GetGameByIdAsync_ExistingId_ReturnsGameWithDetails()
        {
            var dev = new Developer { Id = Guid.NewGuid(), Name = "Dev" };
            _context.Developers.Add(dev);
            
            var game = new Game { Id = Guid.NewGuid(), Title = "Game1", Description = "D", DeveloperId = dev.Id };
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            var result = await _gameService.GetGameByIdAsync(game.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(game.Id));
            Assert.That(result.DeveloperName, Is.EqualTo("Dev"));
        }

        [Test]
        public async Task GetGameByIdAsync_NonExistingId_ReturnsNull()
        {
            var result = await _gameService.GetGameByIdAsync(Guid.NewGuid());
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task UpdateGameAsync_UpdatesGameAndRelationships()
        {
            var game = new Game { Id = Guid.NewGuid(), Title = "Old Title", Description = "D" };
            _context.Games.Add(game);
            
            var newPlatformId = Guid.NewGuid();
            _context.Platforms.Add(new Platform { Id = newPlatformId, Name = "PS5" });
            
            await _context.SaveChangesAsync();

            var dto = new GameDto
            {
                Id = game.Id,
                Title = "New Title",
                SelectedPlatformIds = new List<Guid> { newPlatformId },
                SelectedGenreIds = new List<Guid>()
            };

            var result = await _gameService.UpdateGameAsync(dto);

            Assert.That(result, Is.True);
            var updatedGame = _context.Games.Find(game.Id);
            Assert.That(updatedGame.Title, Is.EqualTo("New Title"));
            Assert.That(_context.GamePlatforms.Any(gp => gp.GameId == game.Id && gp.PlatformId == newPlatformId), Is.True);
        }

        [Test]
        public async Task UpdateGameAsync_NonExistingId_ReturnsFalse()
        {
            var dto = new GameDto { Id = Guid.NewGuid() };
            var result = await _gameService.UpdateGameAsync(dto);
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteGameAsync_ExistingId_DeletesGameAndRelatedData()
        {
            var gameId = Guid.NewGuid();
            _context.Games.Add(new Game { Id = gameId, Title = "To Delete", Description = "D" });
            _context.LibraryItems.Add(new LibraryItem { Id = Guid.NewGuid(), GameId = gameId, UserId = Guid.NewGuid() });
            _context.GameGenres.Add(new GameGenre { GameId = gameId, GenreId = Guid.NewGuid() });
            await _context.SaveChangesAsync();

            await _gameService.DeleteGameAsync(gameId);

            Assert.That(_context.Games.Find(gameId), Is.Null);
            Assert.That(_context.LibraryItems.Any(l => l.GameId == gameId), Is.False);
            Assert.That(_context.GameGenres.Any(gg => gg.GameId == gameId), Is.False);
        }
    }
}
