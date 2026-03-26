using HeatGames.Data.Models;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize] // Само за логнати
    public class WalletController : Controller
    {
        private readonly UserManager<User> _userManager;

        public WalletController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // 🔹 GET: Показва текущия баланс и опциите за зареждане
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            // Подаваме баланса директно към View-то (няма нужда от отделен DTO за едно число)
            return View(user.WalletBalance);
        }

        // 🔹 GET: Формата за въвеждане на карта (получава сумата от Index)
        public IActionResult TopUp(decimal amount)
        {
            if (amount < 5) return RedirectToAction(nameof(Index));

            var model = new AddFundsViewModel { Amount = amount };
            return View(model);
        }

        // 🔹 POST: Обработка на фиктивното плащане
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TopUp(AddFundsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return Challenge();

                // 1. Добавяме парите към профила
                user.WalletBalance += model.Amount;

                // 2. Запазваме промените в базата
                await _userManager.UpdateAsync(user);

                // 3. Слагаме зелено съобщение за успех
                TempData["SuccessMessage"] = $"Успешно заредихте {model.Amount} лв. във вашия портфейл!";

                return RedirectToAction(nameof(Index));
            }

            // Ако има грешка (напр. невалиден CVV), връщаме формата
            return View(model);
        }
    }
}