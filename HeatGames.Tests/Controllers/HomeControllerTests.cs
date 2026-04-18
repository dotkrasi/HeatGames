using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using HeatGamesCore.Services.Interfaces;
using HeatGames.Data.Models;
using HeatGamesWeb.Controllers;
using HeatGamesWeb.Models;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class HomeControllerTests
    {
        private Mock<IGameService> _mockGameService;
        private Mock<IWishlistService> _mockWishlistService;
        private Mock<UserManager<User>> _mockUserManager;
        private Mock<IDeveloperService> _mockDeveloperService;
        private HomeController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockGameService = new Mock<IGameService>();
            _mockWishlistService = new Mock<IWishlistService>();
            _mockDeveloperService = new Mock<IDeveloperService>();
            
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            _controller = new HomeController(_mockGameService.Object, _mockWishlistService.Object, _mockUserManager.Object, _mockDeveloperService.Object);

            var mockSession = new Mock<ISession>();
            var sessionData = JsonSerializer.Serialize(new List<CartItemViewModel>());
            var sessionBytes = System.Text.Encoding.UTF8.GetBytes(sessionData);
            mockSession.Setup(s => s.TryGetValue("ShoppingCart", out sessionBytes)).Returns(true);

            var httpContext = new DefaultHttpContext();
            httpContext.Session = mockSession.Object;
            
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
        public async Task Index_ReturnsViewWithGames()
        {
            var games = new List<GameDto> { new GameDto { Id = Guid.NewGuid(), Title = "Test Game" } };
            _mockGameService.Setup(s => s.GetAllGamesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid?>(), It.IsAny<decimal?>(), It.IsAny<decimal?>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((games, 1));
            
            _mockDeveloperService.Setup(s => s.GetAllDevelopersAsync()).ReturnsAsync(new List<DeveloperDto>());

            var result = await _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<GameViewModel>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(1));
        }

        [Test]
        public void Privacy_ReturnsView()
        {
            var result = _controller.Privacy() as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Faq_ReturnsView()
        {
            var result = _controller.Faq() as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void RefundPolicy_ReturnsView()
        {
            var result = _controller.RefundPolicy() as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void TermsOfService_ReturnsView()
        {
            var result = _controller.TermsOfService() as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Error_ReturnsViewWithErrorViewModel()
        {
            var result = _controller.Error() as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.TypeOf<ErrorViewModel>());
        }
    }
}

