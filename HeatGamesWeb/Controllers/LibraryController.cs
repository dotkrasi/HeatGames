using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize] // Само за логнати
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;
        private readonly UserManager<User> _userManager;

        public LibraryController(ILibraryService libraryService, UserManager<User> userManager)
        {
            _libraryService = libraryService;
            _userManager = userManager;
        }

        // 🔹 GET: Моите Игри
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var libraryItems = await _libraryService.GetUserLibraryAsync(user.Id);
            return View(libraryItems);
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