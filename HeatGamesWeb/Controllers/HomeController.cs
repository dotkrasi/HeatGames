using HeatGames.Core.Services.Interfaces;
using HeatGamesCore.Services.Interfaces;
using HeatGamesWeb.Models;
using HeatGamesWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGamesWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameService _gameService;

        // Инжектираме GameService, за да можем да взимаме игри
        public HomeController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Взимаме данните (които са Tuple: Games + TotalCount)
            var result = await _gameService.GetAllGamesAsync();

            // Превръщаме (Map) в GameViewModel
            // Използваме result.Games вместо result.Item1 за по-добра четимост
            var viewModel = result.Games.Select(g => new GameViewModel
            {
                Id = g.Id,
                Title = g.Title,
                Price = g.Price,
                CoverImageUrl = g.CoverImageUrl,
                Description = g.Description
            }).ToList();

            // 2. Подаваме чистия списък на View-то
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // 🎯 --- НОВИТЕ 3 МЕТОДА ЗА ПРОФЕСИОНАЛНИЯ FOOTER --- 🎯

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult RefundPolicy()
        {
            return View();
        }

        public IActionResult TermsOfService()
        {
            return View();
        }

        // ----------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}