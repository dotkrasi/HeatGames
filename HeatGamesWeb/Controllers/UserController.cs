using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeatGamesWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var profile = await _userService.GetProfileAsync(userId);

            if (profile == null) return NotFound();

            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserProfileDto model)
        {
            if (!ModelState.IsValid) return View("Index", model);

            var result = await _userService.UpdateProfileAsync(model);

            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
                var user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user != null)
                {
                    await _signInManager.RefreshSignInAsync(user);
                }
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}