using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using HeatGamesCore.Services.Interfaces;
using HeatGamesWeb.Extensions;
using HeatGamesWeb.Models;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IWishlistService _wishlistService;
        private readonly UserManager<User> _userManager;
        private readonly IDeveloperService _developerService; // 🎯 ДОБАВЕНО

        // Инжектираме нужните сървиси за Wishlist, Session(Cart) и Developers
        public HomeController(IGameService gameService, IWishlistService wishlistService, UserManager<User> userManager, IDeveloperService developerService)
        {
            _gameService = gameService;
            _wishlistService = wishlistService;
            _userManager = userManager;
            _developerService = developerService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _gameService.GetAllGamesAsync();

            // СТЪПКА 1: Взимаме количката от сесията
            var cart = HttpContext.Session.Get<List<CartItemViewModel>>("ShoppingCart") ?? new List<CartItemViewModel>();
            var cartGameIds = cart.Select(c => c.GameId).ToHashSet();

            // СТЪПКА 2: Взимаме Wishlist-а (ако има логнат потребител)
            var wishlistGameIds = new HashSet<Guid>();
            if (User.Identity?.IsAuthenticated ?? false)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var wishlistItems = await _wishlistService.GetUserWishlistAsync(user.Id);
                    wishlistGameIds = wishlistItems.Select(w => w.GameId).ToHashSet();
                }
            }

            // СТЪПКА 3: Мапваме и "светваме" флаговете
            var viewModel = result.Games.Select(g => new GameViewModel
            {
                Id = g.Id,
                Title = g.Title,
                Price = g.Price,
                CoverImageUrl = g.CoverImageUrl,
                Description = g.Description,
                IsInCart = cartGameIds.Contains(g.Id),
                IsInWishlist = wishlistGameIds.Contains(g.Id)
            }).ToList();

            // 🎯 СТЪПКА 4: Взимаме Топ 3 Разработчици за началната страница
            var allDevelopers = await _developerService.GetAllDevelopersAsync();
            // Взимаме 3 случайни (или първите 3) студия, за да ги покажем в секцията
            ViewBag.TopDevelopers = allDevelopers.OrderBy(d => Guid.NewGuid()).Take(3).ToList();

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult RefundPolicy()
        {
            return View();
        }

        public IActionResult TermsOfService()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}