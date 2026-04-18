using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using HeatGamesCore.Services.Interfaces;
using HeatGames.Data.Models;
using HeatGamesWeb.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HeatGames.Tests.Controllers
{
    [TestFixture]
    public class LibraryControllerTests
    {
        private Mock<ILibraryService> _mockLibraryService;
        private Mock<IGameService> _mockGameService;
        private Mock<UserManager<User>> _mockUserManager;
        private LibraryController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockLibraryService = new Mock<ILibraryService>();
            _mockGameService = new Mock<IGameService>();
            
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            _controller = new LibraryController(_mockLibraryService.Object, _mockUserManager.Object, _mockGameService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task Index_UserNull_ReturnsChallenge()
        {
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((User)null);

            var result = await _controller.Index();

            Assert.That(result, Is.TypeOf<ChallengeResult>());
        }

        [Test]
        public async Task Index_UserLoggedIn_ReturnsViewWithLibraryItems()
        {
            var user = new User { Id = Guid.NewGuid() };
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

            var libraryItems = new List<LibraryItemDto> { new LibraryItemDto { Id = Guid.NewGuid() } };
            _mockLibraryService.Setup(s => s.GetUserLibraryAsync(user.Id)).ReturnsAsync(libraryItems);

            var result = await _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(libraryItems));
        }

        [Test]
        public async Task Download_GameNotFound_ReturnsNotFound()
        {
            _mockGameService.Setup(s => s.GetGameByIdAsync(It.IsAny<Guid>())).ReturnsAsync((GameDto)null);

            var result = await _controller.Download(Guid.NewGuid());

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Download_GameExists_ReturnsFileResult()
        {
            var game = new GameDto { Id = Guid.NewGuid(), Title = "Test Game" };
            _mockGameService.Setup(s => s.GetGameByIdAsync(game.Id)).ReturnsAsync(game);

            var result = await _controller.Download(game.Id) as FileContentResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ContentType, Is.EqualTo("application/zip"));
            Assert.That(result.FileDownloadName, Is.EqualTo("Test Game.zip"));
        }

        [Test]
        public async Task SavePlayTime_InvalidRequest_ReturnsBadRequest()
        {
            var result = await _controller.SavePlayTime(null);
            Assert.That(result, Is.TypeOf<BadRequestResult>());

            var result2 = await _controller.SavePlayTime(new LibraryController.PlayTimeRequest { LibraryItemId = Guid.Empty });
            Assert.That(result2, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task SavePlayTime_ValidRequest_UpdatesAndReturnsOk()
        {
            var request = new LibraryController.PlayTimeRequest { LibraryItemId = Guid.NewGuid(), MinutesToAdd = 10 };
            
            var result = await _controller.SavePlayTime(request);

            Assert.That(result, Is.TypeOf<OkResult>());
            _mockLibraryService.Verify(s => s.UpdatePlayTimeAsync(request.LibraryItemId, 10), Times.Once);
        }
    }
}

