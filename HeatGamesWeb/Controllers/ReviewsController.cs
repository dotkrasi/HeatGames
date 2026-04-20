using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<User> _userManager;

        public ReviewsController(IReviewService reviewService, UserManager<User> userManager)
        {
            _reviewService = reviewService;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Guid gameId, bool isPositive, string comment)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var dto = new ReviewDto
            {
                UserId = user.Id,
                GameId = gameId,
                IsPositive = isPositive,
                Comment = comment
            };

            var success = await _reviewService.AddReviewAsync(dto);

            if (success)
            {
                TempData["SuccessMessage"] = "Благодарим ви! Вашето ревю беше публикувано.";
            }
            else
            {
                TempData["ErrorMessage"] = "Не можете да добавите ревю. Трябва първо да закупите играта от магазина!";
            }

            return RedirectToAction("Details", "Games", new { id = gameId });
        }
    }
}