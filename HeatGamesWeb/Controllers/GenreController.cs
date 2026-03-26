using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return View(genres);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null) return NotFound();

            return View(genre);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreDto model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                await _genreService.CreateGenreAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null) return NotFound();

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GenreDto model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var success = await _genreService.UpdateGenreAsync(model);
                if (!success) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null) return NotFound();

            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _genreService.DeleteGenreAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}