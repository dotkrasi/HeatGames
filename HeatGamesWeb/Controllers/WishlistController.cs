using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize] // Само логнати потребители имат Wishlist
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly UserManager<User> _userManager;

        public WishlistController(IWishlistService wishlistService, UserManager<User> userManager)
        {
            _wishlistService = wishlistService;
            _userManager = userManager;
        }

        // 🔹 GET: Показва всички игри в списъка с желания
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var wishlistItems = await _wishlistService.GetUserWishlistAsync(user.Id);
            return View(wishlistItems);
        }

        // 🔹 POST: Добавя или премахва игра от списъка (Toggle)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(Guid gameId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            await _wishlistService.ToggleWishlistAsync(user.Id, gameId);

            TempData["SuccessMessage"] = "Списъкът с желания беше обновен!";

            // Връщаме потребителя обратно в страницата на играта
            return RedirectToAction("Details", "Games", new { id = gameId });
        }
    }
}