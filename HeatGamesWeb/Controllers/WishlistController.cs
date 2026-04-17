using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using HeatGamesWeb.Extensions;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly UserManager<User> _userManager;
        private readonly ILibraryService _libraryService;
        private const string CartSessionKey = "ShoppingCart";

        public WishlistController(IWishlistService wishlistService, UserManager<User> userManager, ILibraryService libraryService)
        {
            _wishlistService = wishlistService;
            _userManager = userManager;
            _libraryService = libraryService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var wishlistItems = await _wishlistService.GetUserWishlistAsync(user.Id);

            // Използваме новия метод за масово вземане на ID-та, за да филтрираме списъка
            var ownedGameIds = await _libraryService.GetOwnedGameIdsAsync(user.Id);
            var filteredItems = wishlistItems.Where(w => !ownedGameIds.Contains(w.GameId)).ToList();

            return View(filteredItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(Guid gameId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            // 🎯 ИЗПОЛЗВАМЕ ТВОЯ МЕТОД ТУК:
            bool isOwned = await _libraryService.UserOwnsGameAsync(user.Id, gameId);

            if (isOwned)
            {
                TempData["ErrorMessage"] = "You already own this game, it cannot be in your wishlist.";
                return RedirectToAction("Details", "Games", new { id = gameId });
            }

            await _wishlistService.ToggleWishlistAsync(user.Id, gameId);
            return RedirectToAction("Details", "Games", new { id = gameId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(Guid gameId, string title, string coverUrl, decimal price)
        {
            var cart = HttpContext.Session.Get<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();
            if (!cart.Any(c => c.GameId == gameId))
            {
                cart.Add(new CartItemViewModel { GameId = gameId, Title = title, CoverImageUrl = coverUrl, Price = price });
                HttpContext.Session.Set(CartSessionKey, cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAllToCart()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();
            var wishlistItems = await _wishlistService.GetUserWishlistAsync(user.Id);
            var cart = HttpContext.Session.Get<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();
            foreach (var item in wishlistItems)
            {
                if (!cart.Any(c => c.GameId == item.GameId))
                {
                    cart.Add(new CartItemViewModel { GameId = item.GameId, Title = item.GameTitle, CoverImageUrl = item.CoverImageUrl, Price = item.CurrentPrice });
                }
            }
            HttpContext.Session.Set(CartSessionKey, cart);
            return RedirectToAction("Index");
        }
    }
}