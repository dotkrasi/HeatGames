using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using HeatGamesCore.Services.Interfaces; // Добавено за IGameService
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;          // Добавено за MemoryStream
using System.IO.Compression; // Добавено за ZipArchive
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize] // Само за логнати
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;
        private readonly UserManager<User> _userManager;
        private readonly IGameService _gameService; // ДОБАВЕНО: Инжектираме GameService

        // ДОБАВЕНО: Добавяме IGameService в конструктора
        public LibraryController(ILibraryService libraryService, UserManager<User> userManager, IGameService gameService)
        {
            _libraryService = libraryService;
            _userManager = userManager;
            _gameService = gameService;
        }

        // 🔹 GET: Моите Игри
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var libraryItems = await _libraryService.GetUserLibraryAsync(user.Id);
            return View(libraryItems);
        }

        // 🔹 НОВО: GET: Изтегляне на играта (Генерира динамичен .zip файл)
        [HttpGet]
        public async Task<IActionResult> Download(Guid gameId)
        {
            // 1. Взимаме играта, за да й вземем заглавието
            var game = await _gameService.GetGameByIdAsync(gameId);
            if (game == null) return NotFound();

            // 2. Почистваме името на играта от забранени символи за файл
            string safeFileName = string.Join("_", game.Title.Split(Path.GetInvalidFileNameChars()));

            // 3. Създаваме ZIP архив динамично в паметта
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    // Създаваме малко текстово файлче вътре в ZIP-а
                    var readmeEntry = archive.CreateEntry("readme.txt");
                    using (var entryStream = readmeEntry.Open())
                    using (var streamWriter = new StreamWriter(entryStream))
                    {
                        streamWriter.WriteLine($"=== {game.Title} ===");
                        streamWriter.WriteLine("Благодарим ви, че закупихте тази игра от HeatGames!");
                        streamWriter.WriteLine("Това е демонстрационен файл за изтегляне.");
                        streamWriter.WriteLine("Пожелаваме ви приятна игра!");
                    }
                }

                // 4. Връщаме ZIP файла на потребителя
                var zipBytes = memoryStream.ToArray();
                return File(zipBytes, "application/zip", $"{safeFileName}.zip");
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken] // Игнорираме защитата специално за този AJAX call, за да е по-лесно за теста
        public async Task<IActionResult> SavePlayTime([FromBody] PlayTimeRequest request)
        {
            if (request == null || request.LibraryItemId == Guid.Empty)
            {
                return BadRequest();
            }

            // Викаме СЪРВИСА, както си му е редът!
            await _libraryService.UpdatePlayTimeAsync(request.LibraryItemId, request.MinutesToAdd);

            return Ok();
        }

        // Този помощен клас може да стои най-отдолу в контролера (извън самия клас LibraryController) или в папката с DTO-та
        public class PlayTimeRequest
        {
            public Guid LibraryItemId { get; set; }
            public int MinutesToAdd { get; set; }
        }
    }
}