using HeatGames.Data.Models;
using HeatGamesWeb.Controllers;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HeatGames.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<UserManager<User>> _mockUserManager;
        private Mock<SignInManager<User>> _mockSignInManager;
        private AccountController _controller;

        [SetUp]
        public void SetUp()
        {
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            _mockSignInManager = new Mock<SignInManager<User>>(_mockUserManager.Object, contextAccessor.Object, claimsFactory.Object, null, null, null, null);

            _controller = new AccountController(_mockUserManager.Object, _mockSignInManager.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task Register_ValidModel_CreatesUserAndRedirects()
        {
            var model = new RegisterViewModel { Username = "Test", Email = "t@t.com", Password = "Pwd" };
            _mockUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), model.Password)).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(m => m.AddToRoleAsync(It.IsAny<User>(), "User")).ReturnsAsync(IdentityResult.Success);
            _mockSignInManager.Setup(m => m.SignInAsync(It.IsAny<User>(), false, null)).Returns(Task.CompletedTask);

            var result = await _controller.Register(model) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
        }

        [Test]
        public async Task Register_InvalidModel_ReturnsView()
        {
            _controller.ModelState.AddModelError("Email", "Required");
            var model = new RegisterViewModel();

            var result = await _controller.Register(model) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(model));
        }

        [Test]
        public async Task Login_ValidCredentials_RedirectsToHome()
        {
            var model = new LoginViewModel { Username = "Test", Password = "Pwd" };
            _mockSignInManager.Setup(m => m.PasswordSignInAsync(model.Username, model.Password, false, false)).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var result = await _controller.Login(model) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
        }

        [Test]
        public async Task Login_InvalidCredentials_ReturnsViewWithError()
        {
            var model = new LoginViewModel { Username = "Test", Password = "Pwd" };
            _mockSignInManager.Setup(m => m.PasswordSignInAsync(model.Username, model.Password, false, false)).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var result = await _controller.Login(model) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(model));
            Assert.That(_controller.ModelState.ErrorCount, Is.GreaterThan(0));
        }

        [Test]
        public async Task Logout_SignsOutAndRedirectsToHome()
        {
            _mockSignInManager.Setup(m => m.SignOutAsync()).Returns(Task.CompletedTask);

            var result = await _controller.Logout() as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
            _mockSignInManager.Verify(m => m.SignOutAsync(), Times.Once);
        }
    }
}
