using HeatGames.Data.Models;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize]
    public class WalletController : Controller
    {
        private readonly UserManager<User> _userManager;

        public WalletController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            return View(user.WalletBalance);
        }

        public IActionResult TopUp(decimal amount)
        {
            if (amount < 5) return RedirectToAction(nameof(Index));

            var model = new AddFundsViewModel { Amount = amount };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TopUp(AddFundsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return Challenge();

                user.WalletBalance += model.Amount;

                await _userManager.UpdateAsync(user);

                TempData["SuccessMessage"] = $"Успешно заредихте {model.Amount} лв. във вашия портфейл!";

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}