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
        private const string CartSessionKey = "ShoppingCart";

        public WishlistController(IWishlistService wishlistService, UserManager<User> userManager)
        {
            _wishlistService = wishlistService;
            _userManager = userManager;
        }

        // 🔹 GET: Show all games in wishlist
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var wishlistItems = await _wishlistService.GetUserWishlistAsync(user.Id);
            return View(wishlistItems);
        }

        // 🔹 POST: Add/Remove game from wishlist (Toggle)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(Guid gameId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            await _wishlistService.ToggleWishlistAsync(user.Id, gameId);

            TempData["SuccessMessage"] = "Wishlist has been updated!";
            return RedirectToAction("Details", "Games", new { id = gameId });
        }

        // 🔹 POST: Add a single game from wishlist to cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(Guid gameId, string title, string coverUrl, decimal price)
        {
            var cart = HttpContext.Session.Get<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();

            if (!cart.Any(c => c.GameId == gameId))
            {
                cart.Add(new CartItemViewModel
                {
                    GameId = gameId,
                    Title = title,
                    CoverImageUrl = coverUrl,
                    Price = price
                });

                HttpContext.Session.Set(CartSessionKey, cart);
                TempData["SuccessMessage"] = $"{title} added to your cart!";
            }
            else
            {
                TempData["ErrorMessage"] = $"{title} is already in your cart.";
            }

            return RedirectToAction("Index");
        }

        // 🔹 POST: Add ALL wishlist games to cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAllToCart()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var wishlistItems = await _wishlistService.GetUserWishlistAsync(user.Id);
            var cart = HttpContext.Session.Get<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();

            int addedCount = 0;

            foreach (var item in wishlistItems)
            {
                if (!cart.Any(c => c.GameId == item.GameId))
                {
                    cart.Add(new CartItemViewModel
                    {
                        GameId = item.GameId,
                        Title = item.GameTitle,
                        CoverImageUrl = item.CoverImageUrl,
                        Price = item.CurrentPrice
                    });
                    addedCount++;
                }
            }

            if (addedCount > 0)
            {
                HttpContext.Session.Set(CartSessionKey, cart);
                TempData["SuccessMessage"] = $"Successfully added {addedCount} games to your cart!";
            }
            else if (wishlistItems.Any())
            {
                TempData["ErrorMessage"] = "All items in your wishlist are already in your cart.";
            }

            return RedirectToAction("Index");
        }
    }
}