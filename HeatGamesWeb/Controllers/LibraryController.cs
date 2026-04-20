using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using HeatGamesCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;
        private readonly UserManager<User> _userManager;
        private readonly IGameService _gameService;

        public LibraryController(ILibraryService libraryService, UserManager<User> userManager, IGameService gameService)
        {
            _libraryService = libraryService;
            _userManager = userManager;
            _gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var libraryItems = await _libraryService.GetUserLibraryAsync(user.Id);
            return View(libraryItems);
        }

        [HttpGet]
        public async Task<IActionResult> Download(Guid gameId)
        {
            var game = await _gameService.GetGameByIdAsync(gameId);
            if (game == null) return NotFound();

            string safeFileName = string.Join("_", game.Title.Split(Path.GetInvalidFileNameChars()));

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var readmeEntry = archive.CreateEntry("readme.txt");
                    using (var entryStream = readmeEntry.Open())
                    using (var streamWriter = new StreamWriter(entryStream))
                    {
                        streamWriter.WriteLine($"=== {game.Title} ===");
                        streamWriter.WriteLine("Thank you for purchasing this game from HeatGames!");
                        streamWriter.WriteLine("This is a demonstration download file.");
                        streamWriter.WriteLine("Have fun playing!");
                    }
                }

                var zipBytes = memoryStream.ToArray();
                return File(zipBytes, "application/zip", $"{safeFileName}.zip");
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SavePlayTime([FromBody] PlayTimeRequest request)
        {
            if (request == null || request.LibraryItemId == Guid.Empty)
            {
                return BadRequest();
            }

            await _libraryService.UpdatePlayTimeAsync(request.LibraryItemId, request.MinutesToAdd);

            return Ok();
        }

        public class PlayTimeRequest
        {
            public Guid LibraryItemId { get; set; }
            public int MinutesToAdd { get; set; }
        }
    }
}