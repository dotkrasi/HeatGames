using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize] // Само логнати потребители могат да купуват и виждат поръчки
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;

        // Инжектираме OrderService и UserManager (за да вземем кой е логнат в момента)
        public OrdersController(IOrderService orderService, UserManager<User> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        // 🔹 GET: История на поръчките
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var orders = await _orderService.GetUserOrdersAsync(user.Id);
            return View(orders);
        }

        // 🔹 POST: Създаване на поръчка (Купуване на игра)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(Guid gameId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            // Извикваме перлата в короната - нашия OrderService!
            var result = await _orderService.PurchaseGameAsync(user.Id, gameId);

            if (result.Success)
            {
                // Успешна покупка -> Записваме съобщение и пращаме в Библиотеката
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index", "Library");
            }
            else
            {
                // Неуспешна (няма пари или вече я има) -> Връщаме го в страницата на играта с грешка
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Details", "Games", new { id = gameId });
            }
        }
    }
}