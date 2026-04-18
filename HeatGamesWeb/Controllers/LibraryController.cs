using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using HeatGamesCore.Services.Interfaces; // Added for IGameService
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;          // Added for MemoryStream
using System.IO.Compression; // Added for ZipArchive
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize] // Only for logged-in users
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;
        private readonly UserManager<User> _userManager;
        private readonly IGameService _gameService; // ADDED: Injecting GameService

        // ADDED: Adding IGameService to the constructor
        public LibraryController(ILibraryService libraryService, UserManager<User> userManager, IGameService gameService)
        {
            _libraryService = libraryService;
            _userManager = userManager;
            _gameService = gameService;
        }

        // 🔹 GET: My Games
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var libraryItems = await _libraryService.GetUserLibraryAsync(user.Id);
            return View(libraryItems);
        }

        // 🔹 NEW: GET: Download the game (Generates a dynamic .zip file)
        [HttpGet]
        public async Task<IActionResult> Download(Guid gameId)
        {
            // 1. Get the game to retrieve its title
            var game = await _gameService.GetGameByIdAsync(gameId);
            if (game == null) return NotFound();

            // 2. Clean the game title from invalid file characters
            string safeFileName = string.Join("_", game.Title.Split(Path.GetInvalidFileNameChars()));

            // 3. Create a ZIP archive dynamically in memory
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    // Create a small text file inside the ZIP
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

                // 4. Return the ZIP file to the user
                var zipBytes = memoryStream.ToArray();
                return File(zipBytes, "application/zip", $"{safeFileName}.zip");
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken] // Ignoring protection specifically for this AJAX call to make testing easier
        public async Task<IActionResult> SavePlayTime([FromBody] PlayTimeRequest request)
        {
            if (request == null || request.LibraryItemId == Guid.Empty)
            {
                return BadRequest();
            }

            // Call the SERVICE appropriately!
            await _libraryService.UpdatePlayTimeAsync(request.LibraryItemId, request.MinutesToAdd);

            return Ok();
        }

        // This helper class can stay at the bottom of the controller or in the DTOs folder
        public class PlayTimeRequest
        {
            public Guid LibraryItemId { get; set; }
            public int MinutesToAdd { get; set; }
        }
    }
}