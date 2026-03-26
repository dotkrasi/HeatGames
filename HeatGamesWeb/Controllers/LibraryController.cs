using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    }
}