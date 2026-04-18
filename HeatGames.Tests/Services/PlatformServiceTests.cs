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
    public class PlatformServiceTests
    {
        private HeatGamesDbContext _context;
        private PlatformService _platformService;

        [SetUp]
        public void SetUp()
        {
            _context = DbContextHelper.GetInMemoryDbContext();
            _context.Platforms.RemoveRange(_context.Platforms);
            _context.SaveChanges();
            _platformService = new PlatformService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAllPlatformsAsync_ReturnsAllPlatforms()
        {
            _context.Platforms.Add(new Platform { Id = Guid.NewGuid(), Name = "PC" });
            _context.Platforms.Add(new Platform { Id = Guid.NewGuid(), Name = "PS5" });
            await _context.SaveChangesAsync();

            var result = await _platformService.GetAllPlatformsAsync();

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task CreatePlatformAsync_AddsPlatformToDatabase()
        {
            var dto = new PlatformDto { Id = Guid.NewGuid(), Name = "Xbox" };

            await _platformService.CreatePlatformAsync(dto);

            var platformInDb = _context.Platforms.FirstOrDefault(p => p.Id == dto.Id);
            Assert.That(platformInDb, Is.Not.Null);
            Assert.That(platformInDb.Name, Is.EqualTo("Xbox"));
        }

        [Test]
        public async Task GetPlatformByIdAsync_ExistingId_ReturnsPlatform()
        {
            var id = Guid.NewGuid();
            _context.Platforms.Add(new Platform { Id = id, Name = "Switch" });
            await _context.SaveChangesAsync();

            var result = await _platformService.GetPlatformByIdAsync(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task GetPlatformByIdAsync_NonExistingId_ReturnsNull()
        {
            var result = await _platformService.GetPlatformByIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task UpdatePlatformAsync_ExistingId_UpdatesAndReturnsTrue()
        {
            var id = Guid.NewGuid();
            _context.Platforms.Add(new Platform { Id = id, Name = "Old" });
            await _context.SaveChangesAsync();

            var dto = new PlatformDto { Id = id, Name = "New" };

            var result = await _platformService.UpdatePlatformAsync(dto);

            Assert.That(result, Is.True);
            var platformInDb = _context.Platforms.Find(id);
            Assert.That(platformInDb.Name, Is.EqualTo("New"));
        }

        [Test]
        public async Task UpdatePlatformAsync_NonExistingId_ReturnsFalse()
        {
            var dto = new PlatformDto { Id = Guid.NewGuid(), Name = "New" };

            var result = await _platformService.UpdatePlatformAsync(dto);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeletePlatformAsync_ExistingId_DeletesPlatform()
        {
            var id = Guid.NewGuid();
            _context.Platforms.Add(new Platform { Id = id, Name = "ToDelete" });
            await _context.SaveChangesAsync();

            await _platformService.DeletePlatformAsync(id);

            var platformInDb = _context.Platforms.Find(id);
            Assert.That(platformInDb, Is.Null);
        }

        [Test]
        public async Task DeletePlatformAsync_NonExistingId_DoesNothing()
        {
            Assert.DoesNotThrowAsync(async () => await _platformService.DeletePlatformAsync(Guid.NewGuid()));
        }
    }
}
