using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PlatformController : Controller
    {
        private readonly IPlatformService _platformService;

        public PlatformController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        public async Task<IActionResult> Index()
        {
            var platforms = await _platformService.GetAllPlatformsAsync();
            return View(platforms);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlatformDto model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                await _platformService.CreatePlatformAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var platform = await _platformService.GetPlatformByIdAsync(id);
            if (platform == null) return NotFound();
            return View(platform);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PlatformDto model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var success = await _platformService.UpdatePlatformAsync(model);
                if (!success) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var platform = await _platformService.GetPlatformByIdAsync(id);
            if (platform == null) return NotFound();
            return View(platform);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _platformService.DeletePlatformAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}