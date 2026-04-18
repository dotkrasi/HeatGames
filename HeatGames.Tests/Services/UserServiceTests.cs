using HeatGames.Core.DTOs;
using HeatGames.Core.Services;
using HeatGames.Data.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGames.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<UserManager<User>> _mockUserManager;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            _userService = new UserService(_mockUserManager.Object);
        }

        [Test]
        public async Task GetProfileAsync_ExistingUser_ReturnsProfile()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserName = "TestUser", Email = "test@test.com", WalletBalance = 100 };
            _mockUserManager.Setup(m => m.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            var result = await _userService.GetProfileAsync(userId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Username, Is.EqualTo("TestUser"));
            Assert.That(result.Email, Is.EqualTo("test@test.com"));
            Assert.That(result.WalletBalance, Is.EqualTo(100));
        }

        [Test]
        public async Task GetProfileAsync_NonExistingUser_ReturnsNull()
        {
            _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            var result = await _userService.GetProfileAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task UpdateProfileAsync_NonExistingUser_ReturnsFalse()
        {
            var dto = new UserProfileDto { Id = Guid.NewGuid() };
            _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            var result = await _userService.UpdateProfileAsync(dto);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Потребителят не е намерен."));
        }

        [Test]
        public async Task UpdateProfileAsync_UpdatePictureOnly_Succeeds()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, ProfilePictureUrl = "old" };
            var dto = new UserProfileDto { Id = userId, ProfilePictureUrl = "new" };
            
            _mockUserManager.Setup(m => m.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

            var result = await _userService.UpdateProfileAsync(dto);

            Assert.That(result.Success, Is.True);
            Assert.That(user.ProfilePictureUrl, Is.EqualTo("new"));
            _mockUserManager.Verify(m => m.UpdateAsync(user), Times.Once);
        }

        [Test]
        public async Task UpdateProfileAsync_ChangePasswordFails_ReturnsFalse()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId };
            var dto = new UserProfileDto { Id = userId, CurrentPassword = "old", NewPassword = "new" };
            
            _mockUserManager.Setup(m => m.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            var errors = new[] { new IdentityError { Description = "Wrong password" } };
            _mockUserManager.Setup(m => m.ChangePasswordAsync(user, "old", "new")).ReturnsAsync(IdentityResult.Failed(errors));

            var result = await _userService.UpdateProfileAsync(dto);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Wrong password"));
        }

        [Test]
        public async Task UpdateProfileAsync_ChangePasswordSucceeds_UpdatesUser()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId };
            var dto = new UserProfileDto { Id = userId, CurrentPassword = "old", NewPassword = "new" };
            
            _mockUserManager.Setup(m => m.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.ChangePasswordAsync(user, "old", "new")).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(m => m.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

            var result = await _userService.UpdateProfileAsync(dto);

            Assert.That(result.Success, Is.True);
            _mockUserManager.Verify(m => m.ChangePasswordAsync(user, "old", "new"), Times.Once);
            _mockUserManager.Verify(m => m.UpdateAsync(user), Times.Once);
        }
    }
}
