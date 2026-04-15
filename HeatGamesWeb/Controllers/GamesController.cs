using HeatGames.Core.DTOs;
using HeatGames.Core.Services;
using HeatGames.Core.Services.Interfaces;
using HeatGamesCore.Services.Interfaces;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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
        // Инжектираме сървисите
        public GamesController(IGameService gameService, IDeveloperService developerService, IReviewService reviewService, IGenreService genreService, IPlatformService platformService)
        {
            _gameService = gameService;
            _developerService = developerService;
            _reviewService = reviewService;
            _genreService = genreService;
            _platformService = platformService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string? searchQuery, string? genre, decimal? maxPrice, int page = 1)
        {
            int pageSize = 8; // Колко игри да показваме на една страница

            // Сървисът вече ни връща обект (Tuple) с .Games и .TotalCount
            var result = await _gameService.GetAllGamesAsync(searchQuery, genre, maxPrice, page, pageSize);

            // ТУК Е ПОПРАВКАТА: Извикваме .Select върху result.Games!
            var viewModels = result.Games.Select(dto => new GameViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Price = dto.Price,
                CoverImageUrl = dto.CoverImageUrl,
                DeveloperId = dto.DeveloperId
            }).ToList();

            var genres = await _genreService.GetAllGenresAsync();
            ViewBag.Genres = genres;

            // Запазваме състоянието на филтрите във ViewBag
            ViewBag.CurrentSearch = searchQuery;
            ViewBag.CurrentGenre = genre;
            ViewBag.CurrentMaxPrice = maxPrice;

            // ПАГИНАЦИЯТА:
            ViewBag.CurrentPage = page;
            ViewBag.TotalCount = result.TotalCount;
            ViewBag.TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize);

            return View(viewModels);
        }

        // 2. GET: Games/Create (Отваря празната форма)
        public async Task<IActionResult> Create()
        {
            // Взимаме разработчиците и ги пращаме към View-то за падащото меню
            var developers = await _developerService.GetAllDevelopersAsync();
            ViewBag.Developers = new SelectList(developers, "Id", "Name");
            ViewBag.Platforms = await _platformService.GetAllPlatformsAsync();

            return View();
        }

        // 3. POST: Games/Create (Записва данните от формата в базата)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var gameDto = new GameDto
                {
                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    ReleaseDate = model.ReleaseDate,
                    CoverImageUrl = model.CoverImageUrl,
                    DeveloperId = model.DeveloperId,
                    SelectedPlatformIds = model.SelectedPlatformIds // ТОВА Е ВАЖНО
                };

                await _gameService.CreateGameAsync(gameDto);
                return RedirectToAction(nameof(Index)); // Връща ни към списъка с игри
            }

            // Ако има грешка, презареждаме падащото меню
            var developers = await _developerService.GetAllDevelopersAsync();
            ViewBag.Developers = new SelectList(developers, "Id", "Name", model.DeveloperId);

            return View(model);
        }

        [AllowAnonymous]
        // 🔹 DETAILS (GET)
        public async Task<IActionResult> Details(Guid id)
        {
            var gameDto = await _gameService.GetGameByIdAsync(id);
            if (gameDto == null) return NotFound();

            // Мапваме към ViewModel, за да го покажем красиво във View-то
            var viewModel = new GameViewModel
            {
                Id = gameDto.Id,
                Title = gameDto.Title,
                Description = gameDto.Description,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                CoverImageUrl = gameDto.CoverImageUrl,
                DeveloperId = gameDto.DeveloperId,
                Platforms = gameDto.Platforms,
                Genres = gameDto.Genres
            };

            ViewBag.Reviews = await _reviewService.GetGameReviewsAsync(id);

            return View(viewModel);
        }

        // 🔹 EDIT (GET)
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
                DeveloperId = gameDto.DeveloperId
            };

            // Зареждаме разработчиците за падащото меню и маркираме текущия като избран
            var developers = await _developerService.GetAllDevelopersAsync();
            ViewBag.Developers = new SelectList(developers, "Id", "Name", viewModel.DeveloperId);
            ViewBag.Platforms = await _platformService.GetAllPlatformsAsync();

            return View(viewModel);
        }

        // 🔹 EDIT (POST)
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
                    SelectedPlatformIds = model.SelectedPlatformIds // ТОВА Е ВАЖНО
                };

                var success = await _gameService.UpdateGameAsync(gameDto);
                if (!success) return NotFound();

                return RedirectToAction(nameof(Index));
            }

            // Ако има грешка (напр. празно поле), пак трябва да заредим падащото меню!
            var developers = await _developerService.GetAllDevelopersAsync();
            ViewBag.Developers = new SelectList(developers, "Id", "Name", model.DeveloperId);

            return View(model);
        }

        // 🔹 DELETE (GET)
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

        // 🔹 DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _gameService.DeleteGameAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}