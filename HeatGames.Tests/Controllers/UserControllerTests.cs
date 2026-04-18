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
    public class UserControllerTests
    {
        private Mock<IUserService> _mockUserService;
        private Mock<UserManager<User>> _mockUserManager;
        private Mock<SignInManager<User>> _mockSignInManager;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockUserService = new Mock<IUserService>();
            
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            _mockSignInManager = new Mock<SignInManager<User>>(_mockUserManager.Object, contextAccessor.Object, claimsFactory.Object, null, null, null, null);

            _controller = new UserController(_mockUserService.Object, _mockSignInManager.Object, _mockUserManager.Object);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _controller.TempData = tempData;

            var userId = Guid.NewGuid().ToString();
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) });
            httpContext.User = new ClaimsPrincipal(identity);
            
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
        public async Task Index_ProfileNotFound_ReturnsNotFound()
        {
            _mockUserService.Setup(s => s.GetProfileAsync(It.IsAny<Guid>())).ReturnsAsync((UserProfileDto)null);

            var result = await _controller.Index();

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Index_ProfileFound_ReturnsView()
        {
            var profile = new UserProfileDto { Id = Guid.NewGuid() };
            _mockUserService.Setup(s => s.GetProfileAsync(It.IsAny<Guid>())).ReturnsAsync(profile);

            var result = await _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(profile));
        }

        [Test]
        public async Task Update_InvalidModel_ReturnsView()
        {
            _controller.ModelState.AddModelError("Email", "Required");
            var model = new UserProfileDto();

            var result = await _controller.Update(model) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.Model, Is.EqualTo(model));
        }

        [Test]
        public async Task Update_Failure_SetsErrorMessageAndRedirects()
        {
            var model = new UserProfileDto { Id = Guid.NewGuid() };
            _mockUserService.Setup(s => s.UpdateProfileAsync(model)).ReturnsAsync((false, "Error"));

            var result = await _controller.Update(model) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(_controller.TempData.ContainsKey("ErrorMessage"), Is.True);
            Assert.That(_controller.TempData["ErrorMessage"], Is.EqualTo("Error"));
        }

        [Test]
        public async Task Update_Success_SetsSuccessMessageAndRefreshesSignIn()
        {
            var userId = Guid.NewGuid();
            var model = new UserProfileDto { Id = userId };
            _mockUserService.Setup(s => s.UpdateProfileAsync(model)).ReturnsAsync((true, "Success"));
            
            var user = new User { Id = userId };
            _mockUserManager.Setup(m => m.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _mockSignInManager.Setup(m => m.RefreshSignInAsync(user)).Returns(Task.CompletedTask);

            var result = await _controller.Update(model) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(_controller.TempData.ContainsKey("SuccessMessage"), Is.True);
            _mockSignInManager.Verify(m => m.RefreshSignInAsync(user), Times.Once);
        }
    }
}

