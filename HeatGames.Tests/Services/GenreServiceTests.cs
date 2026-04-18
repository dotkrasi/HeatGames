using HeatGames.Core.DTOs;
using HeatGames.Core.Services;
using HeatGames.Data;
using HeatGames.Data.Models;
using HeatGames.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGames.Tests.Services
{
    [TestFixture]
    public class GenreServiceTests
    {
        private HeatGamesDbContext _context;
        private GenreService _genreService;

        [SetUp]
        public void SetUp()
        {
            _context = DbContextHelper.GetInMemoryDbContext();
            _context.Genres.RemoveRange(_context.Genres);
            _context.SaveChanges();
            _genreService = new GenreService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAllGenresAsync_ReturnsAllGenres()
        {
            _context.Genres.Add(new Genre { Id = Guid.NewGuid(), Name = "Action" });
            _context.Genres.Add(new Genre { Id = Guid.NewGuid(), Name = "RPG" });
            await _context.SaveChangesAsync();

            var result = await _genreService.GetAllGenresAsync();

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task CreateGenreAsync_AddsGenreToDatabase()
        {
            var dto = new GenreDto { Id = Guid.NewGuid(), Name = "Strategy" };

            await _genreService.CreateGenreAsync(dto);

            var genreInDb = _context.Genres.FirstOrDefault(g => g.Id == dto.Id);
            Assert.That(genreInDb, Is.Not.Null);
            Assert.That(genreInDb.Name, Is.EqualTo("Strategy"));
        }

        [Test]
        public async Task GetGenreByIdAsync_ExistingId_ReturnsGenre()
        {
            var id = Guid.NewGuid();
            _context.Genres.Add(new Genre { Id = id, Name = "Shooter" });
            await _context.SaveChangesAsync();

            var result = await _genreService.GetGenreByIdAsync(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task GetGenreByIdAsync_NonExistingId_ReturnsNull()
        {
            var result = await _genreService.GetGenreByIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task UpdateGenreAsync_ExistingId_UpdatesAndReturnsTrue()
        {
            var id = Guid.NewGuid();
            _context.Genres.Add(new Genre { Id = id, Name = "Old" });
            await _context.SaveChangesAsync();

            var dto = new GenreDto { Id = id, Name = "New" };

            var result = await _genreService.UpdateGenreAsync(dto);

            Assert.That(result, Is.True);
            var genreInDb = _context.Genres.Find(id);
            Assert.That(genreInDb.Name, Is.EqualTo("New"));
        }

        [Test]
        public async Task UpdateGenreAsync_NonExistingId_ReturnsFalse()
        {
            var dto = new GenreDto { Id = Guid.NewGuid(), Name = "New" };

            var result = await _genreService.UpdateGenreAsync(dto);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteGenreAsync_ExistingId_DeletesGenre()
        {
            var id = Guid.NewGuid();
            _context.Genres.Add(new Genre { Id = id, Name = "ToDelete" });
            await _context.SaveChangesAsync();

            await _genreService.DeleteGenreAsync(id);

            var genreInDb = _context.Genres.Find(id);
            Assert.That(genreInDb, Is.Null);
        }

        [Test]
        public async Task DeleteGenreAsync_NonExistingId_DoesNothing()
        {
            Assert.DoesNotThrowAsync(async () => await _genreService.DeleteGenreAsync(Guid.NewGuid()));
        }
    }
}
