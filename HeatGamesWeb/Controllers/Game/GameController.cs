using HeatGamesWeb.Services.Interfaces;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HeatGamesWeb.Controllers.Game
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        // Инжектираме Service слоя, който ще се грижи за логиката и базата
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: Games (Списък с всички игри)
        public async Task<IActionResult> Index()
        {
            var games = await _gameService.GetAllGamesAsync();
            return View(games);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var game = await _gameService.GetGameByIdAsync(id.Value);
            if (game == null) return NotFound();

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            // Забележка: По-късно тук ще трябва да подадем ViewBag или SelectList 
            // с разработчиците (Developers), за да напълним DropDown менюто във View-то.
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Генерираме ново Guid, ако не използваме автоматично в базата
                model.Id = Guid.NewGuid();

                await _gameService.CreateGameAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var model = await _gameService.GetGameByIdAsync(id.Value);
            if (model == null) return NotFound();

            return View(model);
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var success = await _gameService.UpdateGameAsync(model);
                if (!success) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var model = await _gameService.GetGameByIdAsync(id.Value);
            if (model == null) return NotFound();

            return View(model);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _gameService.DeleteGameAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
