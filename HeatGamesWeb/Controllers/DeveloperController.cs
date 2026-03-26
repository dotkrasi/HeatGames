using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HeatGamesWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DeveloperController : Controller
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        // 🔹 READ ALL (Списък)
        public async Task<IActionResult> Index()
        {
            var developers = await _developerService.GetAllDevelopersAsync();
            return View(developers);
        }

        // 🔹 DETAILS
        public async Task<IActionResult> Details(Guid id)
        {
            var developer = await _developerService.GetDeveloperByIdAsync(id);
            if (developer == null) return NotFound();

            return View(developer);
        }

        // 🔹 CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // 🔹 CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeveloperDto model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                await _developerService.CreateDeveloperAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // 🔹 EDIT (GET)
        public async Task<IActionResult> Edit(Guid id)
        {
            var developer = await _developerService.GetDeveloperByIdAsync(id);
            if (developer == null) return NotFound();

            return View(developer);
        }

        // 🔹 EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DeveloperDto model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var success = await _developerService.UpdateDeveloperAsync(model);
                if (!success) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // 🔹 DELETE (GET)
        public async Task<IActionResult> Delete(Guid id)
        {
            var developer = await _developerService.GetDeveloperByIdAsync(id);
            if (developer == null) return NotFound();

            return View(developer);
        }

        // 🔹 DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _developerService.DeleteDeveloperAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}