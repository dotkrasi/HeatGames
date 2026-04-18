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
    public class GenreControllerTests
    {
        private Mock<IGenreService> _mockGenreService;
        private GenreController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockGenreService = new Mock<IGenreService>();
            _controller = new GenreController(_mockGenreService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task Index_ReturnsViewWithGenres()
        {
            var genres = new List<GenreDto> { new GenreDto { Id = Guid.NewGuid(), Name = "RPG" } };
            _mockGenreService.Setup(s => s.GetAllGenresAsync()).ReturnsAsync(genres);

            var result = await _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(genres));
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
            var model = new GenreDto { Name = "Action" };

            var result = await _controller.Create(model) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(GenreController.Index)));
            _mockGenreService.Verify(s => s.CreateGenreAsync(It.IsAny<GenreDto>()), Times.Once);
        }

        [Test]
        public async Task Create_Post_InvalidModel_ReturnsView()
        {
            _controller.ModelState.AddModelError("Name", "Required");
            var model = new GenreDto();

            var result = await _controller.Create(model) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(model));
        }

        [Test]
        public async Task Edit_Get_ExistingId_ReturnsView()
        {
            var id = Guid.NewGuid();
            var genre = new GenreDto { Id = id, Name = "RPG" };
            _mockGenreService.Setup(s => s.GetGenreByIdAsync(id)).ReturnsAsync(genre);

            var result = await _controller.Edit(id) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(genre));
        }

        [Test]
        public async Task Edit_Get_NonExistingId_ReturnsNotFound()
        {
            _mockGenreService.Setup(s => s.GetGenreByIdAsync(It.IsAny<Guid>())).ReturnsAsync((GenreDto)null);

            var result = await _controller.Edit(Guid.NewGuid());

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_IdMismatch_ReturnsNotFound()
        {
            var result = await _controller.Edit(Guid.NewGuid(), new GenreDto { Id = Guid.NewGuid() });

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_ValidModelAndSuccess_RedirectsToIndex()
        {
            var id = Guid.NewGuid();
            var model = new GenreDto { Id = id, Name = "Action" };
            _mockGenreService.Setup(s => s.UpdateGenreAsync(model)).ReturnsAsync(true);

            var result = await _controller.Edit(id, model) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(GenreController.Index)));
        }

        [Test]
        public async Task Edit_Post_ValidModelAndFailure_ReturnsNotFound()
        {
            var id = Guid.NewGuid();
            var model = new GenreDto { Id = id, Name = "Action" };
            _mockGenreService.Setup(s => s.UpdateGenreAsync(model)).ReturnsAsync(false);

            var result = await _controller.Edit(id, model);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_ExistingId_ReturnsView()
        {
            var id = Guid.NewGuid();
            var genre = new GenreDto { Id = id, Name = "RPG" };
            _mockGenreService.Setup(s => s.GetGenreByIdAsync(id)).ReturnsAsync(genre);

            var result = await _controller.Delete(id) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(genre));
        }

        [Test]
        public async Task Delete_Get_NonExistingId_ReturnsNotFound()
        {
            _mockGenreService.Setup(s => s.GetGenreByIdAsync(It.IsAny<Guid>())).ReturnsAsync((GenreDto)null);

            var result = await _controller.Delete(Guid.NewGuid());

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteConfirmed_ValidId_RedirectsToIndex()
        {
            var id = Guid.NewGuid();

            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(GenreController.Index)));
            _mockGenreService.Verify(s => s.DeleteGenreAsync(id), Times.Once);
        }
    }
}
