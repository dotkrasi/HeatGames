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
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeatGames.Tests.Controllers
{
    [TestFixture]
    public class OrdersControllerTests
    {
        private Mock<IOrderService> _mockOrderService;
        private Mock<UserManager<User>> _mockUserManager;
        private OrdersController _controller;
        private Mock<ISession> _mockSession;

        [SetUp]
        public void SetUp()
        {
            _mockOrderService = new Mock<IOrderService>();
            
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            _controller = new OrdersController(_mockOrderService.Object, _mockUserManager.Object);

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
        public async Task Index_ReturnsViewWithOrders()
        {
            var user = new User { Id = Guid.NewGuid() };
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            
            var orders = new List<OrderDto> { new OrderDto { Id = Guid.NewGuid() } };
            _mockOrderService.Setup(s => s.GetUserOrdersAsync(user.Id)).ReturnsAsync(orders);

            var result = await _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(orders));
        }

        [Test]
        public async Task Buy_Success_RedirectsToLibrary()
        {
            var user = new User { Id = Guid.NewGuid() };
            var gameId = Guid.NewGuid();
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _mockOrderService.Setup(s => s.PurchaseGameAsync(user.Id, gameId)).ReturnsAsync((true, "Success"));

            var result = await _controller.Buy(gameId) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Library"));
        }

        [Test]
        public async Task Buy_Failure_RedirectsToDetails()
        {
            var user = new User { Id = Guid.NewGuid() };
            var gameId = Guid.NewGuid();
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _mockOrderService.Setup(s => s.PurchaseGameAsync(user.Id, gameId)).ReturnsAsync((false, "Fail"));

            var result = await _controller.Buy(gameId) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            Assert.That(result.ControllerName, Is.EqualTo("Games"));
            Assert.That(_controller.TempData.ContainsKey("ErrorMessage"), Is.True);
        }

        [Test]
        public async Task Checkout_EmptyCart_RedirectsToCart()
        {
            var user = new User { Id = Guid.NewGuid() };
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            
            byte[] emptyData = null;
            _mockSession.Setup(s => s.TryGetValue("ShoppingCart", out emptyData)).Returns(false);

            var result = await _controller.Checkout() as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Cart"));
        }

        [Test]
        public async Task Checkout_NotEnoughFunds_RedirectsToCart()
        {
            var user = new User { Id = Guid.NewGuid(), WalletBalance = 10 };
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            
            var cart = new List<CartItemViewModel> { new CartItemViewModel { Price = 20 } };
            var sessionData = JsonSerializer.Serialize(cart);
            var sessionBytes = System.Text.Encoding.UTF8.GetBytes(sessionData);
            _mockSession.Setup(s => s.TryGetValue("ShoppingCart", out sessionBytes)).Returns(true);

            var result = await _controller.Checkout() as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Cart"));
            Assert.That(_controller.TempData.ContainsKey("ErrorMessage"), Is.True);
        }

        [Test]
        public async Task Checkout_Success_RedirectsToLibrary()
        {
            var user = new User { Id = Guid.NewGuid(), WalletBalance = 100 };
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            
            var cart = new List<CartItemViewModel> { new CartItemViewModel { GameId = Guid.NewGuid(), Price = 20 } };
            var sessionData = JsonSerializer.Serialize(cart);
            var sessionBytes = System.Text.Encoding.UTF8.GetBytes(sessionData);
            _mockSession.Setup(s => s.TryGetValue("ShoppingCart", out sessionBytes)).Returns(true);

            _mockOrderService.Setup(s => s.PurchaseGameAsync(user.Id, It.IsAny<Guid>())).ReturnsAsync((true, "Ok"));

            var result = await _controller.Checkout() as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Library"));
            _mockSession.Verify(s => s.Remove("ShoppingCart"), Times.Once);
        }

        [Test]
        public async Task Manage_ReturnsAllOrders()
        {
            var orders = new List<OrderDto> { new OrderDto { Id = Guid.NewGuid() } };
            _mockOrderService.Setup(s => s.GetAllOrdersAsync()).ReturnsAsync(orders);

            var result = await _controller.Manage() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(orders));
        }
    }
}

