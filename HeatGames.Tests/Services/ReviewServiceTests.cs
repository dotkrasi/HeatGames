using HeatGames.Core.DTOs;
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
    public class ReviewServiceTests
    {
        private HeatGamesDbContext _context;
        private ReviewService _reviewService;

        [SetUp]
        public void SetUp()
        {
            _context = DbContextHelper.GetInMemoryDbContext();
            
            _context.Reviews.RemoveRange(_context.Reviews);
            _context.LibraryItems.RemoveRange(_context.LibraryItems);
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();
            
            _reviewService = new ReviewService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetGameReviewsAsync_ReturnsReviewsForGame()
        {
            var gameId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            _context.Users.Add(new User { Id = userId, UserName = "Reviewer" });
            _context.Reviews.Add(new Review { Id = 1, GameId = gameId, UserId = userId, Comment = "Good", IsPositive = true, CreatedOn = DateTime.UtcNow });
            await _context.SaveChangesAsync();

            var result = await _reviewService.GetGameReviewsAsync(gameId);

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().UserName, Is.EqualTo("Reviewer"));
            Assert.That(result.First().Comment, Is.EqualTo("Good"));
        }

        [Test]
        public async Task AddReviewAsync_UserOwnsGame_AddsReviewAndReturnsTrue()
        {
            var gameId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            _context.LibraryItems.Add(new LibraryItem { Id = Guid.NewGuid(), GameId = gameId, UserId = userId });
            await _context.SaveChangesAsync();

            var dto = new ReviewDto { GameId = gameId, UserId = userId, Comment = "Nice", IsPositive = true };
            var result = await _reviewService.AddReviewAsync(dto);

            Assert.That(result, Is.True);
            Assert.That(_context.Reviews.Any(r => r.GameId == gameId && r.UserId == userId), Is.True);
        }

        [Test]
        public async Task AddReviewAsync_UserDoesNotOwnGame_ReturnsFalse()
        {
            var dto = new ReviewDto { GameId = Guid.NewGuid(), UserId = Guid.NewGuid(), Comment = "Nice", IsPositive = true };
            var result = await _reviewService.AddReviewAsync(dto);

            Assert.That(result, Is.False);
            Assert.That(_context.Reviews.Any(r => r.GameId == dto.GameId && r.UserId == dto.UserId), Is.False);
        }
    }
}
