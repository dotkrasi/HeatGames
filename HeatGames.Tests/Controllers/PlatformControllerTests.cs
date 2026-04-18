using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using HeatGamesWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeatGames.Tests.Controllers
{
    [TestFixture]
    public class PlatformControllerTests
    {
        private Mock<IPlatformService> _mockPlatformService;
        private PlatformController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockPlatformService = new Mock<IPlatformService>();
            _controller = new PlatformController(_mockPlatformService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task Index_ReturnsViewWithPlatforms()
        {
            var platforms = new List<PlatformDto> { new PlatformDto { Id = Guid.NewGuid(), Name = "PC" } };
            _mockPlatformService.Setup(s => s.GetAllPlatformsAsync()).ReturnsAsync(platforms);

            var result = await _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(platforms));
        }

        [Test]
        public void Create_Get_ReturnsView()
        {
            var result = _controller.Create() as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
            var model = new PlatformDto { Name = "PC" };

            var result = await _controller.Create(model) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(PlatformController.Index)));
            _mockPlatformService.Verify(s => s.CreatePlatformAsync(It.IsAny<PlatformDto>()), Times.Once);
        }

        [Test]
        public async Task Create_Post_InvalidModel_ReturnsView()
        {
            _controller.ModelState.AddModelError("Name", "Required");
            var model = new PlatformDto();

            var result = await _controller.Create(model) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(model));
        }

        [Test]
        public async Task Edit_Get_ExistingId_ReturnsView()
        {
            var id = Guid.NewGuid();
            var platform = new PlatformDto { Id = id, Name = "PC" };
            _mockPlatformService.Setup(s => s.GetPlatformByIdAsync(id)).ReturnsAsync(platform);

            var result = await _controller.Edit(id) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(platform));
        }

        [Test]
        public async Task Edit_Get_NonExistingId_ReturnsNotFound()
        {
            _mockPlatformService.Setup(s => s.GetPlatformByIdAsync(It.IsAny<Guid>())).ReturnsAsync((PlatformDto)null);

            var result = await _controller.Edit(Guid.NewGuid());

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_IdMismatch_ReturnsNotFound()
        {
            var result = await _controller.Edit(Guid.NewGuid(), new PlatformDto { Id = Guid.NewGuid() });

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_ValidModelAndSuccess_RedirectsToIndex()
        {
            var id = Guid.NewGuid();
            var model = new PlatformDto { Id = id, Name = "PC" };
            _mockPlatformService.Setup(s => s.UpdatePlatformAsync(model)).ReturnsAsync(true);

            var result = await _controller.Edit(id, model) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(PlatformController.Index)));
        }

        [Test]
        public async Task Edit_Post_ValidModelAndFailure_ReturnsNotFound()
        {
            var id = Guid.NewGuid();
            var model = new PlatformDto { Id = id, Name = "PC" };
            _mockPlatformService.Setup(s => s.UpdatePlatformAsync(model)).ReturnsAsync(false);

            var result = await _controller.Edit(id, model);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_ExistingId_ReturnsView()
        {
            var id = Guid.NewGuid();
            var platform = new PlatformDto { Id = id, Name = "PC" };
            _mockPlatformService.Setup(s => s.GetPlatformByIdAsync(id)).ReturnsAsync(platform);

            var result = await _controller.Delete(id) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(platform));
        }

        [Test]
        public async Task Delete_Get_NonExistingId_ReturnsNotFound()
        {
            _mockPlatformService.Setup(s => s.GetPlatformByIdAsync(It.IsAny<Guid>())).ReturnsAsync((PlatformDto)null);

            var result = await _controller.Delete(Guid.NewGuid());

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteConfirmed_ValidId_RedirectsToIndex()
        {
            var id = Guid.NewGuid();

            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(PlatformController.Index)));
            _mockPlatformService.Verify(s => s.DeletePlatformAsync(id), Times.Once);
        }
    }
}
