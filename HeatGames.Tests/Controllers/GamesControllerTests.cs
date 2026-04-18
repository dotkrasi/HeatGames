using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using HeatGamesCore.Services.Interfaces;
using HeatGames.Data.Models;
using HeatGamesWeb.Controllers;
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
    public class GamesControllerTests
    {
        private Mock<IGameService> _mockGameService;
        private Mock<IDeveloperService> _mockDeveloperService;
        private Mock<IReviewService> _mockReviewService;
        private Mock<IGenreService> _mockGenreService;
        private Mock<IPlatformService> _mockPlatformService;
        private Mock<IWishlistService> _mockWishlistService;
        private Mock<ILibraryService> _mockLibraryService;
        private Mock<UserManager<User>> _mockUserManager;
        private GamesController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockGameService = new Mock<IGameService>();
            _mockDeveloperService = new Mock<IDeveloperService>();
            _mockReviewService = new Mock<IReviewService>();
            _mockGenreService = new Mock<IGenreService>();
            _mockPlatformService = new Mock<IPlatformService>();
            _mockWishlistService = new Mock<IWishlistService>();
            _mockLibraryService = new Mock<ILibraryService>();
            
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            _controller = new GamesController(
                _mockGameService.Object,
                _mockDeveloperService.Object,
                _mockReviewService.Object,
                _mockGenreService.Object,
                _mockPlatformService.Object,
                _mockWishlistService.Object,
                _mockLibraryService.Object,
                _mockUserManager.Object
            );

            // Mock Session
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
            
            _mockGenreService.Setup(s => s.GetAllGenresAsync()).ReturnsAsync(new List<GenreDto>());
            _mockDeveloperService.Setup(s => s.GetAllDevelopersAsync()).ReturnsAsync(new List<DeveloperDto>());

            var result = await _controller.Index(null, null, null, null, null, 1) as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<GameViewModel>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(1));
            Assert.That(model[0].Title, Is.EqualTo("Test Game"));
        }

        [Test]
        public async Task Details_ExistingId_ReturnsView()
        {
            var id = Guid.NewGuid();
            var game = new GameDto { Id = id, Title = "Test" };
            _mockGameService.Setup(s => s.GetGameByIdAsync(id)).ReturnsAsync(game);
            _mockReviewService.Setup(s => s.GetGameReviewsAsync(id)).ReturnsAsync(new List<ReviewDto>());

            var result = await _controller.Details(id) as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as GameViewModel;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task Details_NonExistingId_ReturnsNotFound()
        {
            _mockGameService.Setup(s => s.GetGameByIdAsync(It.IsAny<Guid>())).ReturnsAsync((GameDto)null);

            var result = await _controller.Details(Guid.NewGuid());

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Create_Get_ReturnsView()
        {
            _mockDeveloperService.Setup(s => s.GetAllDevelopersAsync()).ReturnsAsync(new List<DeveloperDto>());
            _mockPlatformService.Setup(s => s.GetAllPlatformsAsync()).ReturnsAsync(new List<PlatformDto>());
            _mockGenreService.Setup(s => s.GetAllGenresAsync()).ReturnsAsync(new List<GenreDto>());

            var result = await _controller.Create() as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as GameViewModel;
            Assert.That(model, Is.Not.Null);
        }

        [Test]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
            var model = new GameViewModel { Title = "Test" };

            var result = await _controller.Create(model) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(GamesController.Index)));
            _mockGameService.Verify(s => s.CreateGameAsync(It.IsAny<GameDto>()), Times.Once);
        }

        [Test]
        public async Task Create_Post_InvalidModel_ReturnsView()
        {
            _controller.ModelState.AddModelError("Title", "Required");
            var model = new GameViewModel();

            var result = await _controller.Create(model) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(model));
            _mockGameService.Verify(s => s.CreateGameAsync(It.IsAny<GameDto>()), Times.Never);
        }

        [Test]
        public async Task Delete_Get_ExistingId_ReturnsView()
        {
            var id = Guid.NewGuid();
            var game = new GameDto { Id = id, Title = "Test" };
            _mockGameService.Setup(s => s.GetGameByIdAsync(id)).ReturnsAsync(game);

            var result = await _controller.Delete(id) as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as GameViewModel;
            Assert.That(model.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task DeleteConfirmed_ValidId_RedirectsToIndex()
        {
            var id = Guid.NewGuid();

            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(GamesController.Index)));
            _mockGameService.Verify(s => s.DeleteGameAsync(id), Times.Once);
        }
    }
}
