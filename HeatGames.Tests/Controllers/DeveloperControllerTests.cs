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
    public class DeveloperControllerTests
    {
        private Mock<IDeveloperService> _mockDeveloperService;
        private DeveloperController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockDeveloperService = new Mock<IDeveloperService>();
            _controller = new DeveloperController(_mockDeveloperService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task Index_ReturnsViewWithDevelopers()
        {
            var developers = new List<DeveloperDto> { new DeveloperDto { Id = Guid.NewGuid(), Name = "Dev1" } };
            _mockDeveloperService.Setup(s => s.GetAllDevelopersAsync()).ReturnsAsync(developers);

            var result = await _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(developers));
        }

        [Test]
        public async Task Details_ExistingId_ReturnsViewWithDeveloper()
        {
            var id = Guid.NewGuid();
            var developer = new DeveloperDto { Id = id, Name = "Dev" };
            _mockDeveloperService.Setup(s => s.GetDeveloperByIdAsync(id)).ReturnsAsync(developer);

            var result = await _controller.Details(id) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(developer));
        }

        [Test]
        public async Task Details_NonExistingId_ReturnsNotFound()
        {
            _mockDeveloperService.Setup(s => s.GetDeveloperByIdAsync(It.IsAny<Guid>())).ReturnsAsync((DeveloperDto)null);

            var result = await _controller.Details(Guid.NewGuid());

            Assert.That(result, Is.TypeOf<NotFoundResult>());
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
            var model = new DeveloperDto { Name = "New Dev" };

            var result = await _controller.Create(model) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(DeveloperController.Index)));
            _mockDeveloperService.Verify(s => s.CreateDeveloperAsync(It.IsAny<DeveloperDto>()), Times.Once);
        }

        [Test]
        public async Task Create_Post_InvalidModel_ReturnsViewWithModel()
        {
            _controller.ModelState.AddModelError("Name", "Required");
            var model = new DeveloperDto();

            var result = await _controller.Create(model) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(model));
            _mockDeveloperService.Verify(s => s.CreateDeveloperAsync(It.IsAny<DeveloperDto>()), Times.Never);
        }

        [Test]
        public async Task Edit_Get_ExistingId_ReturnsView()
        {
            var id = Guid.NewGuid();
            var developer = new DeveloperDto { Id = id, Name = "Dev" };
            _mockDeveloperService.Setup(s => s.GetDeveloperByIdAsync(id)).ReturnsAsync(developer);

            var result = await _controller.Edit(id) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(developer));
        }

        [Test]
        public async Task Edit_Get_NonExistingId_ReturnsNotFound()
        {
            _mockDeveloperService.Setup(s => s.GetDeveloperByIdAsync(It.IsAny<Guid>())).ReturnsAsync((DeveloperDto)null);

            var result = await _controller.Edit(Guid.NewGuid());

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_IdMismatch_ReturnsNotFound()
        {
            var id = Guid.NewGuid();
            var model = new DeveloperDto { Id = Guid.NewGuid() };

            var result = await _controller.Edit(id, model);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_ValidModelAndSuccess_RedirectsToIndex()
        {
            var id = Guid.NewGuid();
            var model = new DeveloperDto { Id = id, Name = "Dev" };
            _mockDeveloperService.Setup(s => s.UpdateDeveloperAsync(model)).ReturnsAsync(true);

            var result = await _controller.Edit(id, model) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(DeveloperController.Index)));
        }

        [Test]
        public async Task Edit_Post_ValidModelAndFailure_ReturnsNotFound()
        {
            var id = Guid.NewGuid();
            var model = new DeveloperDto { Id = id, Name = "Dev" };
            _mockDeveloperService.Setup(s => s.UpdateDeveloperAsync(model)).ReturnsAsync(false);

            var result = await _controller.Edit(id, model);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_InvalidModel_ReturnsViewWithModel()
        {
            var id = Guid.NewGuid();
            var model = new DeveloperDto { Id = id };
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Edit(id, model) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(model));
        }

        [Test]
        public async Task Delete_Get_ExistingId_ReturnsView()
        {
            var id = Guid.NewGuid();
            var developer = new DeveloperDto { Id = id, Name = "Dev" };
            _mockDeveloperService.Setup(s => s.GetDeveloperByIdAsync(id)).ReturnsAsync(developer);

            var result = await _controller.Delete(id) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(developer));
        }

        [Test]
        public async Task Delete_Get_NonExistingId_ReturnsNotFound()
        {
            _mockDeveloperService.Setup(s => s.GetDeveloperByIdAsync(It.IsAny<Guid>())).ReturnsAsync((DeveloperDto)null);

            var result = await _controller.Delete(Guid.NewGuid());

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteConfirmed_ValidId_RedirectsToIndex()
        {
            var id = Guid.NewGuid();

            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(DeveloperController.Index)));
            _mockDeveloperService.Verify(s => s.DeleteDeveloperAsync(id), Times.Once);
        }
    }
}
