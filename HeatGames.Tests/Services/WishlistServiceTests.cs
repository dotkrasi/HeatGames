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
    public class WishlistServiceTests
    {
        private HeatGamesDbContext _context;
        private WishlistService _wishlistService;

        [SetUp]
        public void SetUp()
        {
            _context = DbContextHelper.GetInMemoryDbContext();
            
            _context.Wishlists.RemoveRange(_context.Wishlists);
            _context.Games.RemoveRange(_context.Games);
            _context.SaveChanges();
            
            _wishlistService = new WishlistService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetUserWishlistAsync_ReturnsUserWishlistItems()
        {
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();
            _context.Games.Add(new Game { Id = gameId, Title = "Game", Description = "D", Price = 10 });
            _context.Wishlists.Add(new Wishlist { Id = Guid.NewGuid(), UserId = userId, GameId = gameId, AddedOn = DateTime.UtcNow });
            await _context.SaveChangesAsync();

            var result = await _wishlistService.GetUserWishlistAsync(userId);

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().GameTitle, Is.EqualTo("Game"));
            Assert.That(result.First().CurrentPrice, Is.EqualTo(10));
        }

        [Test]
        public async Task ToggleWishlistAsync_GameNotInWishlist_AddsToWishlist()
        {
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();

            var result = await _wishlistService.ToggleWishlistAsync(userId, gameId);

            Assert.That(result, Is.True);
            Assert.That(_context.Wishlists.Any(w => w.UserId == userId && w.GameId == gameId), Is.True);
        }

        [Test]
        public async Task ToggleWishlistAsync_GameInWishlist_RemovesFromWishlist()
        {
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();
            _context.Wishlists.Add(new Wishlist { Id = Guid.NewGuid(), UserId = userId, GameId = gameId });
            await _context.SaveChangesAsync();

            var result = await _wishlistService.ToggleWishlistAsync(userId, gameId);

            Assert.That(result, Is.True);
            Assert.That(_context.Wishlists.Any(w => w.UserId == userId && w.GameId == gameId), Is.False);
        }
    }
}
