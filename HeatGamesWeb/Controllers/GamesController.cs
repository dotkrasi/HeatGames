using HeatGames.Core.DTOs;
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
        // Инжектираме сървисите
        public GamesController(IGameService gameService, IDeveloperService developerService, IReviewService reviewService)
        {
            _gameService = gameService;
            _developerService = developerService;
            _reviewService = reviewService;
        }

        [AllowAnonymous]
        // 1. GET: Games (Показва всички игри)
        public async Task<IActionResult> Index()
        {
            var gamesDto = await _gameService.GetAllGamesAsync();

            var viewModels = gamesDto.Select(dto => new GameViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Price = dto.Price,
                CoverImageUrl = dto.CoverImageUrl,
                DeveloperId = dto.DeveloperId
            }).ToList();

            return View(viewModels);
        }

        // 2. GET: Games/Create (Отваря празната форма)
        public async Task<IActionResult> Create()
        {
            // Взимаме разработчиците и ги пращаме към View-то за падащото меню
            var developers = await _developerService.GetAllDevelopersAsync();
            ViewBag.Developers = new SelectList(developers, "Id", "Name");

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
                    DeveloperId = model.DeveloperId
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
                DeveloperId = gameDto.DeveloperId
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
                    DeveloperId = model.DeveloperId
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