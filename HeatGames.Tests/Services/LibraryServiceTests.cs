using HeatGames.Core.Services;
using HeatGames.Data;
using HeatGames.Data.Models;
using HeatGames.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGames.Tests.Services
{
    [TestFixture]
    public class LibraryServiceTests
    {
        private HeatGamesDbContext _context;
        private LibraryService _libraryService;

        [SetUp]
        public void SetUp()
        {
            _context = DbContextHelper.GetInMemoryDbContext();
            
            _context.LibraryItems.RemoveRange(_context.LibraryItems);
            _context.Games.RemoveRange(_context.Games);
            _context.SaveChanges();
            
            _libraryService = new LibraryService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetUserLibraryAsync_ReturnsUserLibraryItems()
        {
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();
            _context.Games.Add(new Game { Id = gameId, Title = "Game", Description = "D" });
            _context.LibraryItems.Add(new LibraryItem { Id = Guid.NewGuid(), UserId = userId, GameId = gameId, PlayTimeMinutes = 60 });
            await _context.SaveChangesAsync();

            var result = await _libraryService.GetUserLibraryAsync(userId);

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().GameTitle, Is.EqualTo("Game"));
            Assert.That(result.First().PlayTimeMinutes, Is.EqualTo(60));
        }

        [Test]
        public async Task UserOwnsGameAsync_OwnsGame_ReturnsTrue()
        {
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();
            _context.LibraryItems.Add(new LibraryItem { Id = Guid.NewGuid(), UserId = userId, GameId = gameId });
            await _context.SaveChangesAsync();

            var result = await _libraryService.UserOwnsGameAsync(userId, gameId);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task UserOwnsGameAsync_DoesNotOwnGame_ReturnsFalse()
        {
            var result = await _libraryService.UserOwnsGameAsync(Guid.NewGuid(), Guid.NewGuid());
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task UpdatePlayTimeAsync_ExistingItem_UpdatesPlayTime()
        {
            var libraryItemId = Guid.NewGuid();
            _context.LibraryItems.Add(new LibraryItem { Id = libraryItemId, PlayTimeMinutes = 10 });
            await _context.SaveChangesAsync();

            await _libraryService.UpdatePlayTimeAsync(libraryItemId, 20);

            var itemInDb = _context.LibraryItems.Find(libraryItemId);
            Assert.That(itemInDb.PlayTimeMinutes, Is.EqualTo(30));
        }

        [Test]
        public async Task UpdatePlayTimeAsync_NonExistingItem_DoesNothing()
        {
            Assert.DoesNotThrowAsync(async () => await _libraryService.UpdatePlayTimeAsync(Guid.NewGuid(), 20));
        }

        [Test]
        public async Task GetOwnedGameIdsAsync_ReturnsIds()
        {
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();
            _context.LibraryItems.Add(new LibraryItem { Id = Guid.NewGuid(), UserId = userId, GameId = gameId });
            await _context.SaveChangesAsync();

            var result = await _libraryService.GetOwnedGameIdsAsync(userId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(gameId));
        }
    }
}
