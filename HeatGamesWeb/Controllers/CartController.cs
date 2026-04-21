using HeatGames.Core.Services.Interfaces; // Change it if the path to your interfaces is different
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
                    TempData["SuccessMessage"] = "The game has been added to the cart!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "This game is already in your cart!";
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
                TempData["SuccessMessage"] = "The game was removed.";
            }

            return RedirectToAction("Index");
        }
    }
}