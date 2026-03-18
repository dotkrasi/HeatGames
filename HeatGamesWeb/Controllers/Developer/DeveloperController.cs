using HeatGames.Data;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeatGamesWeb.Controllers.Developer
{
    public class DeveloperController : Controller
    {
        private readonly HeatGamesDbContext _context;

        public DeveloperController(HeatGamesDbContext context)
        {
            _context = context;
        }

        // 🔹 READ ALL
        public async Task<IActionResult> Index()
        {
            var developers = await _context.Developers
                .Select(d => new DeveloperViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Website = d.Website
                })
                .ToListAsync();

            return View(developers);
        }

        // 🔹 DETAILS
        public async Task<IActionResult> Details(Guid id)
        {
            var developer = await _context.Developers
                .Where(d => d.Id == id)
                .Select(d => new DeveloperViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Website = d.Website
                })
                .FirstOrDefaultAsync();

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
        public async Task<IActionResult> Create(DeveloperViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var developer = new HeatGames.Data.Models.Developer
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Website = model.Website
            };

            await _context.Developers.AddAsync(developer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // 🔹 EDIT (GET)
        public async Task<IActionResult> Edit(Guid id)
        {
            var developer = await _context.Developers.FindAsync(id);

            if (developer == null) return NotFound();

            var model = new DeveloperViewModel
            {
                Id = developer.Id,
                Name = developer.Name,
                Website = developer.Website
            };

            return View(model);
        }

        // 🔹 EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, DeveloperViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var developer = await _context.Developers.FindAsync(id);

            if (developer == null) return NotFound();

            developer.Name = model.Name;
            developer.Website = model.Website;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // 🔹 DELETE (GET)
        public async Task<IActionResult> Delete(Guid id)
        {
            var developer = await _context.Developers.FindAsync(id);

            if (developer == null) return NotFound();

            var model = new DeveloperViewModel
            {
                Id = developer.Id,
                Name = developer.Name,
                Website = developer.Website
            };

            return View(model);
        }

        // 🔹 DELETE (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var developer = await _context.Developers.FindAsync(id);

            if (developer == null) return NotFound();

            _context.Developers.Remove(developer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
