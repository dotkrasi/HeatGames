using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using HeatGamesWeb.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HeatGames.Tests.Controllers
{
    [TestFixture]
    public class ReviewsControllerTests
    {
        private Mock<IReviewService> _mockReviewService;
        private Mock<UserManager<User>> _mockUserManager;
        private ReviewsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockReviewService = new Mock<IReviewService>();
            
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            _controller = new ReviewsController(_mockReviewService.Object, _mockUserManager.Object);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _controller.TempData = tempData;
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task Add_UserNull_ReturnsChallenge()
        {
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((User)null);

            var result = await _controller.Add(Guid.NewGuid(), true, "Test");

            Assert.That(result, Is.TypeOf<ChallengeResult>());
        }

        [Test]
        public async Task Add_ReviewServiceReturnsTrue_SetsSuccessMessageAndRedirects()
        {
            var user = new User { Id = Guid.NewGuid() };
            var gameId = Guid.NewGuid();
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _mockReviewService.Setup(s => s.AddReviewAsync(It.IsAny<ReviewDto>())).ReturnsAsync(true);

            var result = await _controller.Add(gameId, true, "Good") as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            Assert.That(result.ControllerName, Is.EqualTo("Games"));
            Assert.That(_controller.TempData.ContainsKey("SuccessMessage"), Is.True);
        }

        [Test]
        public async Task Add_ReviewServiceReturnsFalse_SetsErrorMessageAndRedirects()
        {
            var user = new User { Id = Guid.NewGuid() };
            var gameId = Guid.NewGuid();
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _mockReviewService.Setup(s => s.AddReviewAsync(It.IsAny<ReviewDto>())).ReturnsAsync(false);

            var result = await _controller.Add(gameId, true, "Good") as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            Assert.That(_controller.TempData.ContainsKey("ErrorMessage"), Is.True);
        }
    }
}

