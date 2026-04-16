using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using HeatGamesWeb.Extensions; // За сесията (SessionExtensions)
using HeatGamesWeb.ViewModels; // За CartItemViewModel
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize] // Само логнати потребители могат да купуват и виждат поръчки
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;

        // Инжектираме OrderService и UserManager
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

        // 🔹 POST: Създаване на поръчка (Директно купуване на 1 игра)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(Guid gameId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var result = await _orderService.PurchaseGameAsync(user.Id, gameId);

            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index", "Library");
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Details", "Games", new { id = gameId });
            }
        }

        // 🔹 POST: ПЛАЩАНЕ НА ЦЯЛАТА КОЛИЧКА (Checkout)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            // Взимаме количката от сесията
            var cart = HttpContext.Session.Get<List<CartItemViewModel>>("ShoppingCart");

            if (cart == null || !cart.Any())
            {
                TempData["ErrorMessage"] = "Количката ви е празна.";
                return RedirectToAction("Index", "Cart");
            }

            // Проверяваме предварително дали има достатъчно пари за ВСИЧКИ игри
            decimal totalAmount = cart.Sum(i => i.Price);
            if (user.WalletBalance < totalAmount)
            {
                TempData["ErrorMessage"] = $"Нямате достатъчно средства. Баланс: {user.WalletBalance:0.00} лв. Нужни: {totalAmount:0.00} лв.";
                return RedirectToAction("Index", "Cart");
            }

            bool hasError = false;
            string lastError = "";

            // Купуваме всяка игра от количката
            foreach (var item in cart)
            {
                var result = await _orderService.PurchaseGameAsync(user.Id, item.GameId);
                if (!result.Success)
                {
                    hasError = true;
                    lastError = result.Message;
                }
            }

            if (hasError)
            {
                TempData["ErrorMessage"] = "Възникна проблем с някои игри: " + lastError;
            }
            else
            {
                TempData["SuccessMessage"] = "Успешно закупихте всички игри от количката!";
                HttpContext.Session.Remove("ShoppingCart"); // Изчистваме количката след успева покупка
            }

            return RedirectToAction("Index", "Library");
        }

        // 🔹 GET: Всички поръчки (САМО ЗА АДМИНИ)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var allOrders = await _orderService.GetAllOrdersAsync();
            return View(allOrders);
        }
    }
}