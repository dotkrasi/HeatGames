using HeatGames.Data.Models;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // 🔹 РЕГИСТРАЦИЯ
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    WalletBalance = 0m // Новите потребители започват с 0 лв.
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Автоматично му даваме роля "User" (обикновен потребител)
                    await _userManager.AddToRoleAsync(user, "User");

                    // Логваме го автоматично след регистрация
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // 🔹 ВХОД (LOGIN)
        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Ако идва от заключена страница (напр. /Games/Create), го връщаме там
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Грешно потребителско име или парола.");
            }
            return View(model);
        }

        // 🔹 ИЗХОД (LOGOUT)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}