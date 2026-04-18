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
    public class DeveloperServiceTests
    {
        private HeatGamesDbContext _context;
        private DeveloperService _developerService;

        [SetUp]
        public void SetUp()
        {
            _context = DbContextHelper.GetInMemoryDbContext();
            _context.Developers.RemoveRange(_context.Developers);
            _context.SaveChanges();
            _developerService = new DeveloperService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAllDevelopersAsync_ReturnsAllDevelopers()
        {
            // Arrange
            _context.Developers.Add(new Developer { Id = Guid.NewGuid(), Name = "Dev1", Website = "web1" });
            _context.Developers.Add(new Developer { Id = Guid.NewGuid(), Name = "Dev2", Website = "web2" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _developerService.GetAllDevelopersAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task CreateDeveloperAsync_AddsDeveloperToDatabase()
        {
            // Arrange
            var dto = new DeveloperDto { Id = Guid.NewGuid(), Name = "NewDev", Website = "NewWeb" };

            // Act
            await _developerService.CreateDeveloperAsync(dto);

            // Assert
            var devInDb = _context.Developers.FirstOrDefault(d => d.Id == dto.Id);
            Assert.That(devInDb, Is.Not.Null);
            Assert.That(devInDb.Name, Is.EqualTo("NewDev"));
        }

        [Test]
        public async Task GetDeveloperByIdAsync_ExistingId_ReturnsDeveloper()
        {
            // Arrange
            var id = Guid.NewGuid();
            _context.Developers.Add(new Developer { Id = id, Name = "Dev", Website = "web" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _developerService.GetDeveloperByIdAsync(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task GetDeveloperByIdAsync_NonExistingId_ReturnsNull()
        {
            // Act
            var result = await _developerService.GetDeveloperByIdAsync(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task UpdateDeveloperAsync_ExistingId_UpdatesAndReturnsTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            _context.Developers.Add(new Developer { Id = id, Name = "Old", Website = "OldWeb" });
            await _context.SaveChangesAsync();

            var dto = new DeveloperDto { Id = id, Name = "New", Website = "NewWeb" };

            // Act
            var result = await _developerService.UpdateDeveloperAsync(dto);

            // Assert
            Assert.That(result, Is.True);
            var devInDb = _context.Developers.Find(id);
            Assert.That(devInDb.Name, Is.EqualTo("New"));
        }

        [Test]
        public async Task UpdateDeveloperAsync_NonExistingId_ReturnsFalse()
        {
            // Arrange
            var dto = new DeveloperDto { Id = Guid.NewGuid(), Name = "New", Website = "NewWeb" };

            // Act
            var result = await _developerService.UpdateDeveloperAsync(dto);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteDeveloperAsync_ExistingId_DeletesDeveloper()
        {
            // Arrange
            var id = Guid.NewGuid();
            _context.Developers.Add(new Developer { Id = id, Name = "Dev", Website = "web" });
            await _context.SaveChangesAsync();

            // Act
            await _developerService.DeleteDeveloperAsync(id);

            // Assert
            var devInDb = _context.Developers.Find(id);
            Assert.That(devInDb, Is.Null);
        }

        [Test]
        public async Task DeleteDeveloperAsync_NonExistingId_DoesNothing()
        {
            // Act
            Assert.DoesNotThrowAsync(async () => await _developerService.DeleteDeveloperAsync(Guid.NewGuid()));
        }
    }
}
