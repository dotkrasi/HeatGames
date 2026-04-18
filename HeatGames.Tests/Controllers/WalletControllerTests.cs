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
using System.Security.Claims;
using System.Threading.Tasks;

namespace HeatGames.Tests.Controllers
{
    [TestFixture]
    public class WalletControllerTests
    {
        private Mock<UserManager<User>> _mockUserManager;
        private WalletController _controller;

        [SetUp]
        public void SetUp()
        {
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            _controller = new WalletController(_mockUserManager.Object);

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
        public async Task Index_ReturnsViewWithBalance()
        {
            var user = new User { Id = Guid.NewGuid(), WalletBalance = 50 };
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

            var result = await _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(50m));
        }

        [Test]
        public async Task Index_UserNull_ReturnsChallenge()
        {
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((User)null);

            var result = await _controller.Index();

            Assert.That(result, Is.TypeOf<ChallengeResult>());
        }

        [Test]
        public void TopUp_Get_AmountLessThan5_RedirectsToIndex()
        {
            var result = _controller.TopUp(4) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(WalletController.Index)));
        }

        [Test]
        public void TopUp_Get_ValidAmount_ReturnsView()
        {
            var result = _controller.TopUp(10) as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as AddFundsViewModel;
            Assert.That(model.Amount, Is.EqualTo(10m));
        }

        [Test]
        public async Task TopUp_Post_ValidModel_UpdatesBalanceAndRedirects()
        {
            var user = new User { Id = Guid.NewGuid(), WalletBalance = 50 };
            _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

            var model = new AddFundsViewModel { Amount = 20 };
            var result = await _controller.TopUp(model) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(WalletController.Index)));
            Assert.That(user.WalletBalance, Is.EqualTo(70m));
            Assert.That(_controller.TempData.ContainsKey("SuccessMessage"), Is.True);
        }

        [Test]
        public async Task TopUp_Post_InvalidModel_ReturnsView()
        {
            _controller.ModelState.AddModelError("CardNumber", "Required");
            var model = new AddFundsViewModel { Amount = 20 };

            var result = await _controller.TopUp(model) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(model));
        }
    }
}

