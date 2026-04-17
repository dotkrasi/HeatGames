using HeatGames.Core.DTOs;
using HeatGames.Core.Services;
using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using HeatGamesCore.Services.Interfaces;
using HeatGamesWeb.Extensions;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IDeveloperService _developerService;
        private readonly IReviewService _reviewService;
        private readonly IGenreService _genreService;
        private readonly IPlatformService _platformService;
        private readonly IWishlistService _wishlistService;
        private readonly ILibraryService _libraryService; // 🎯 ДОБАВЕНО
        private readonly UserManager<User> _userManager;

        public GamesController(
            IGameService gameService,
            IDeveloperService developerService,
            IReviewService reviewService,
            IGenreService genreService,
            IPlatformService platformService,
            IWishlistService wishlistService,
            ILibraryService libraryService, // 🎯 ДОБАВЕНО
            UserManager<User> userManager)
        {
            _gameService = gameService;
            _developerService = developerService;
            _reviewService = reviewService;
            _genreService = genreService;
            _platformService = platformService;
            _wishlistService = wishlistService;
            _libraryService = libraryService; // 🎯
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string? searchQuery, string? genre, Guid? developerId, decimal? minPrice, decimal? maxPrice, int page = 1)
        {
            int pageSize = 16;
            var result = await _gameService.GetAllGamesAsync(searchQuery, genre, developerId, minPrice, maxPrice, page, pageSize);

            var cart = HttpContext.Session.Get<List<CartItemViewModel>>("ShoppingCart") ?? new List<CartItemViewModel>();
            var cartGameIds = cart.Select(c => c.GameId).ToHashSet();

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

            var viewModels = result.Games.Select(dto => new GameViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Price = dto.Price,
                CoverImageUrl = dto.CoverImageUrl,
                DeveloperId = dto.DeveloperId,
                DeveloperName = dto.DeveloperName,
                Platforms = dto.Platforms,
                Genres = dto.Genres,
                IsInCart = cartGameIds.Contains(dto.Id),
                IsInWishlist = wishlistGameIds.Contains(dto.Id)
            }).ToList();

            var genres = await _genreService.GetAllGenresAsync();
            ViewBag.Genres = genres;
            var developers = await _developerService.GetAllDevelopersAsync();
            ViewBag.Developers = developers;

            ViewBag.CurrentSearch = searchQuery;
            ViewBag.CurrentGenre = genre;
            ViewBag.CurrentDeveloperId = developerId;
            ViewBag.CurrentMinPrice = minPrice;
            ViewBag.CurrentMaxPrice = maxPrice;
            ViewBag.CurrentPage = page;
            ViewBag.TotalCount = result.TotalCount;
            ViewBag.TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize);

            return View(viewModels);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var gameDto = await _gameService.GetGameByIdAsync(id);
            if (gameDto == null) return NotFound();

            bool isInWishlist = false;
            bool ownsGame = false;

            if (User.Identity?.IsAuthenticated ?? false)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var wishlist = await _wishlistService.GetUserWishlistAsync(user.Id);
                    isInWishlist = wishlist.Any(w => w.GameId == id);

                    // 🎯 ТУК БЕШЕ ГРЕШКАТА - ОПРАВЕНО Е С ТВОЯ МЕТОД:
                    ownsGame = await _libraryService.UserOwnsGameAsync(user.Id, id);
                }
            }

            var cart = HttpContext.Session.Get<List<CartItemViewModel>>("ShoppingCart") ?? new List<CartItemViewModel>();
            bool isInCart = cart.Any(c => c.GameId == id);

            var viewModel = new GameViewModel
            {
                Id = gameDto.Id,
                Title = gameDto.Title,
                Description = gameDto.Description,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                CoverImageUrl = gameDto.CoverImageUrl,
                DeveloperName = gameDto.DeveloperName,
                Genres = gameDto.Genres,
                Platforms = gameDto.Platforms,
                IsInCart = isInCart,
                IsInWishlist = isInWishlist
            };

            ViewBag.OwnsGame = ownsGame;
            ViewBag.Reviews = await _reviewService.GetGameReviewsAsync(id);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var gameDto = await _gameService.GetGameByIdAsync(id);
            if (gameDto == null) return NotFound();

            var viewModel = new GameViewModel
            {
                Id = gameDto.Id,
                Title = gameDto.Title,
                Description = gameDto.Description,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                CoverImageUrl = gameDto.CoverImageUrl,
                DeveloperId = gameDto.DeveloperId,
                SelectedPlatformIds = gameDto.SelectedPlatformIds,
                SelectedGenreIds = gameDto.SelectedGenreIds
            };

            var developers = await _developerService.GetAllDevelopersAsync();
            ViewBag.Developers = new SelectList(developers, "Id", "Name", viewModel.DeveloperId);
            ViewBag.Platforms = await _platformService.GetAllPlatformsAsync();
            ViewBag.Genres = await _genreService.GetAllGenresAsync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var gameDto = new GameDto
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    ReleaseDate = model.ReleaseDate,
                    CoverImageUrl = model.CoverImageUrl,
                    DeveloperId = model.DeveloperId,
                    SelectedPlatformIds = model.SelectedPlatformIds,
                    SelectedGenreIds = model.SelectedGenreIds
                };
                var success = await _gameService.UpdateGameAsync(gameDto);
                if (!success) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            var developers = await _developerService.GetAllDevelopersAsync();
            ViewBag.Developers = new SelectList(developers, "Id", "Name", model.DeveloperId);
            ViewBag.Platforms = await _platformService.GetAllPlatformsAsync();
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var gameDto = await _gameService.GetGameByIdAsync(id);
            if (gameDto == null) return NotFound();

            var viewModel = new GameViewModel
            {
                Id = gameDto.Id,
                Title = gameDto.Title,
                Price = gameDto.Price,
                CoverImageUrl = gameDto.CoverImageUrl
            };
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _gameService.DeleteGameAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}