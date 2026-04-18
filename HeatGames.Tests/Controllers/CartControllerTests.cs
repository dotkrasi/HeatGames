using HeatGames.Core.DTOs;
using HeatGamesCore.Services.Interfaces;
using HeatGamesWeb.Controllers;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeatGames.Tests.Controllers
{
    [TestFixture]
    public class CartControllerTests
    {
        private Mock<IGameService> _mockGameService;
        private CartController _controller;
        private Mock<ISession> _mockSession;

        [SetUp]
        public void SetUp()
        {
            _mockGameService = new Mock<IGameService>();
            _controller = new CartController(_mockGameService.Object);

            _mockSession = new Mock<ISession>();
            
            var httpContext = new DefaultHttpContext();
            httpContext.Session = _mockSession.Object;
            
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
        public void Index_ReturnsViewWithCart()
        {
            var cart = new List<CartItemViewModel> { new CartItemViewModel { GameId = Guid.NewGuid() } };
            var sessionData = JsonSerializer.Serialize(cart);
            var sessionBytes = System.Text.Encoding.UTF8.GetBytes(sessionData);
            _mockSession.Setup(s => s.TryGetValue("ShoppingCart", out sessionBytes)).Returns(true);

            var result = _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<CartItemViewModel>;
            Assert.That(model.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Add_GameNotInCart_AddsGameAndRedirects()
        {
            byte[] emptyData = null;
            _mockSession.Setup(s => s.TryGetValue("ShoppingCart", out emptyData)).Returns(false);

            var gameId = Guid.NewGuid();
            var game = new GameDto { Id = gameId, Title = "Test Game" };
            _mockGameService.Setup(s => s.GetGameByIdAsync(gameId)).ReturnsAsync(game);

            var result = await _controller.Add(gameId) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            Assert.That(_controller.TempData.ContainsKey("SuccessMessage"), Is.True);
            _mockSession.Verify(s => s.Set("ShoppingCart", It.IsAny<byte[]>()), Times.Once);
        }

        [Test]
        public async Task Add_GameAlreadyInCart_SetsErrorAndRedirects()
        {
            var gameId = Guid.NewGuid();
            var cart = new List<CartItemViewModel> { new CartItemViewModel { GameId = gameId } };
            var sessionData = JsonSerializer.Serialize(cart);
            var sessionBytes = System.Text.Encoding.UTF8.GetBytes(sessionData);
            _mockSession.Setup(s => s.TryGetValue("ShoppingCart", out sessionBytes)).Returns(true);

            var result = await _controller.Add(gameId) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            Assert.That(_controller.TempData.ContainsKey("ErrorMessage"), Is.True);
            _mockSession.Verify(s => s.Set("ShoppingCart", It.IsAny<byte[]>()), Times.Never);
        }

        [Test]
        public void Remove_GameInCart_RemovesAndRedirects()
        {
            var gameId = Guid.NewGuid();
            var cart = new List<CartItemViewModel> { new CartItemViewModel { GameId = gameId } };
            var sessionData = JsonSerializer.Serialize(cart);
            var sessionBytes = System.Text.Encoding.UTF8.GetBytes(sessionData);
            _mockSession.Setup(s => s.TryGetValue("ShoppingCart", out sessionBytes)).Returns(true);

            var result = _controller.Remove(gameId) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(_controller.TempData.ContainsKey("SuccessMessage"), Is.True);
            _mockSession.Verify(s => s.Set("ShoppingCart", It.IsAny<byte[]>()), Times.Once);
        }
    }
}

