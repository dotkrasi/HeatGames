using HeatGames.Core.Services.Interfaces; // Смени го, ако пътят към интерфейсите ти е друг
using HeatGamesCore.Services.Interfaces;
using HeatGamesWeb.Extensions;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeatGamesWeb.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IGameService _gameService;
        private const string CartSessionKey = "ShoppingCart";

        public CartController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid gameId)
        {
            var cart = HttpContext.Session.Get<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();

            if (!cart.Any(c => c.GameId == gameId))
            {
                var game = await _gameService.GetGameByIdAsync(gameId);

                if (game != null)
                {
                    cart.Add(new CartItemViewModel
                    {
                        GameId = game.Id,
                        Title = game.Title,
                        CoverImageUrl = game.CoverImageUrl,
                        Price = game.Price
                    });

                    HttpContext.Session.Set(CartSessionKey, cart);
                    TempData["SuccessMessage"] = "Играта е добавена в количката!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Тази игра вече е в количката ви!";
            }

            return RedirectToAction("Details", "Games", new { id = gameId });
        }

        [HttpPost]
        public IActionResult Remove(Guid gameId)
        {
            var cart = HttpContext.Session.Get<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();
            var itemToRemove = cart.FirstOrDefault(c => c.GameId == gameId);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.Set(CartSessionKey, cart);
                TempData["SuccessMessage"] = "Играта беше премахната.";
            }

            return RedirectToAction("Index");
        }
    }
}