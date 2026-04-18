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
    public class OrderServiceTests
    {
        private HeatGamesDbContext _context;
        private OrderService _orderService;

        [SetUp]
        public void SetUp()
        {
            _context = DbContextHelper.GetInMemoryDbContext();
            
            _context.Users.RemoveRange(_context.Users);
            _context.Games.RemoveRange(_context.Games);
            _context.LibraryItems.RemoveRange(_context.LibraryItems);
            _context.Orders.RemoveRange(_context.Orders);
            _context.OrderItems.RemoveRange(_context.OrderItems);
            _context.Wishlists.RemoveRange(_context.Wishlists);
            _context.SaveChanges();
            
            _orderService = new OrderService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task PurchaseGameAsync_UserOrGameNotFound_ReturnsFalse()
        {
            var result = await _orderService.PurchaseGameAsync(Guid.NewGuid(), Guid.NewGuid());
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Потребителят или играта не бяха намерени."));
        }

        [Test]
        public async Task PurchaseGameAsync_UserAlreadyOwnsGame_ReturnsFalse()
        {
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();
            _context.Users.Add(new User { Id = userId, WalletBalance = 100 });
            _context.Games.Add(new Game { Id = gameId, Title = "T", Description = "D", Price = 50 });
            _context.LibraryItems.Add(new LibraryItem { Id = Guid.NewGuid(), UserId = userId, GameId = gameId });
            await _context.SaveChangesAsync();

            var result = await _orderService.PurchaseGameAsync(userId, gameId);
            
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Вече притежавате тази игра във вашата библиотека."));
        }

        [Test]
        public async Task PurchaseGameAsync_NotEnoughFunds_ReturnsFalse()
        {
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();
            _context.Users.Add(new User { Id = userId, WalletBalance = 10 });
            _context.Games.Add(new Game { Id = gameId, Title = "T", Description = "D", Price = 50 });
            await _context.SaveChangesAsync();

            var result = await _orderService.PurchaseGameAsync(userId, gameId);
            
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Нямате достатъчно средства в портфейла."));
        }

        [Test]
        public async Task PurchaseGameAsync_ValidPurchase_Succeeds()
        {
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();
            _context.Users.Add(new User { Id = userId, WalletBalance = 100 });
            _context.Games.Add(new Game { Id = gameId, Title = "T", Description = "D", Price = 50 });
            _context.Wishlists.Add(new Wishlist { UserId = userId, GameId = gameId });
            await _context.SaveChangesAsync();

            var result = await _orderService.PurchaseGameAsync(userId, gameId);
            
            Assert.That(result.Success, Is.True);
            
            var user = _context.Users.Find(userId);
            Assert.That(user.WalletBalance, Is.EqualTo(50));
            
            Assert.That(_context.Orders.Any(o => o.UserId == userId), Is.True);
            Assert.That(_context.LibraryItems.Any(l => l.UserId == userId && l.GameId == gameId), Is.True);
            Assert.That(_context.Wishlists.Any(w => w.UserId == userId && w.GameId == gameId), Is.False);
        }

        [Test]
        public async Task GetUserOrdersAsync_ReturnsOrdersForUser()
        {
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();
            _context.Games.Add(new Game { Id = gameId, Title = "Game", Description = "D" });
            
            var orderId = Guid.NewGuid();
            _context.Orders.Add(new Order { Id = orderId, UserId = userId, OrderDate = DateTime.UtcNow });
            _context.OrderItems.Add(new OrderItem { Id = Guid.NewGuid(), OrderId = orderId, GameId = gameId });
            await _context.SaveChangesAsync();

            var result = await _orderService.GetUserOrdersAsync(userId);
            
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().OrderItems.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllOrdersAsync_ReturnsAllOrdersWithUserNames()
        {
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();
            _context.Users.Add(new User { Id = userId, UserName = "Buyer" });
            _context.Games.Add(new Game { Id = gameId, Title = "Game", Description = "D" });
            
            var orderId = Guid.NewGuid();
            _context.Orders.Add(new Order { Id = orderId, UserId = userId, OrderDate = DateTime.UtcNow });
            _context.OrderItems.Add(new OrderItem { Id = Guid.NewGuid(), OrderId = orderId, GameId = gameId });
            await _context.SaveChangesAsync();

            var result = await _orderService.GetAllOrdersAsync();
            
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().UserName, Is.EqualTo("Buyer"));
        }
    }
}
