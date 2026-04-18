using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using HeatGamesWeb.Controllers;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeatGames.Tests.Controllers
{
    [TestFixture]
    public class WishlistControllerTests
    {
        private Mock<IWishlistService> _mockWishlistService;
        private Mock<UserManager<User>> _mockUserManager;
        private Mock<ILibraryService> _mockLibraryService;
        private WishlistController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockWishlistService = new Mock<IWishlistService>();
            _mockLibraryService = new Mock<ILibraryService>();
            
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            _controller = new WishlistController(_mockWishlistService.Object, _mockUserManager.Object, _mockLibraryService.Object);

            var mockSession = new Mock<ISession>();
            var sessionData = JsonSerializer.Serialize(new List<CartItemViewModel>());
            var sessionBytes = System.Text.Encoding.UTF8.GetBytes(sessionData);
            mockSession.Setup(s => s.TryGetValue("ShoppingCart", out sessionBytes)).Returns(true);

            var httpContext = new DefaultHttpContext();
            httpContext.Session = mockSession.Object;
            
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _controller.TempData = tempData;

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task Index_ReturnsViewWithFilteredItems()
        {
            var user = new User { Id = Guid.NewGuid() };
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

            var gameId1 = Guid.NewGuid(); // Owned
            var gameId2 = Guid.NewGuid(); // Not owned
            
            var wishlist = new List<WishlistDto>
            {
                new WishlistDto { GameId = gameId1 },
                new WishlistDto { GameId = gameId2 }
            };
            
            _mockWishlistService.Setup(s => s.GetUserWishlistAsync(user.Id)).ReturnsAsync(wishlist);
            _mockLibraryService.Setup(s => s.GetOwnedGameIdsAsync(user.Id)).ReturnsAsync(new List<Guid> { gameId1 });

            var result = await _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<WishlistDto>;
            Assert.That(model.Count, Is.EqualTo(1));
            Assert.That(model.First().GameId, Is.EqualTo(gameId2));
        }

        [Test]
        public async Task Toggle_UserOwnsGame_RedirectsToDetailsWithError()
        {
            var user = new User { Id = Guid.NewGuid() };
            var gameId = Guid.NewGuid();
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _mockLibraryService.Setup(s => s.UserOwnsGameAsync(user.Id, gameId)).ReturnsAsync(true);

            var result = await _controller.Toggle(gameId) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            Assert.That(result.ControllerName, Is.EqualTo("Games"));
            Assert.That(_controller.TempData.ContainsKey("ErrorMessage"), Is.True);
        }

        [Test]
        public async Task Toggle_UserDoesNotOwnGame_TogglesAndRedirects()
        {
            var user = new User { Id = Guid.NewGuid() };
            var gameId = Guid.NewGuid();
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _mockLibraryService.Setup(s => s.UserOwnsGameAsync(user.Id, gameId)).ReturnsAsync(false);
            _mockWishlistService.Setup(s => s.ToggleWishlistAsync(user.Id, gameId)).ReturnsAsync(true);

            var result = await _controller.Toggle(gameId) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            _mockWishlistService.Verify(s => s.ToggleWishlistAsync(user.Id, gameId), Times.Once);
        }

        [Test]
        public void AddToCart_AddsItemAndRedirects()
        {
            var gameId = Guid.NewGuid();
            var result = _controller.AddToCart(gameId, "Test", "url", 10) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task AddAllToCart_AddsAllWishlistItemsAndRedirects()
        {
            var user = new User { Id = Guid.NewGuid() };
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            
            var wishlist = new List<WishlistDto> { new WishlistDto { GameId = Guid.NewGuid() } };
            _mockWishlistService.Setup(s => s.GetUserWishlistAsync(user.Id)).ReturnsAsync(wishlist);

            var result = await _controller.AddAllToCart() as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }
    }
}

