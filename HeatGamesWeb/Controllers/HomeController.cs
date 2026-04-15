using HeatGames.Core.Services.Interfaces; // Увери се, че това е твоят namespace за интерфейсите
using HeatGamesCore.Services.Interfaces;
using HeatGamesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

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
        // 1. Взимаме данните (които са Tuple: игри + бройка)
        var result = await _gameService.GetAllGamesAsync();

        // result.Item1 са игрите (IEnumerable<GameDto>)
        // Трябва да ги превърнем (Map) в GameViewModel
        var viewModel = result.Item1.Select(g => new HeatGamesWeb.ViewModels.GameViewModel
        {
            Id = g.Id,
            Title = g.Title,
            Price = g.Price,
            CoverImageUrl = g.CoverImageUrl,
            Description = g.Description,
            // Добави и другите свойства, ако са нужни за началната страница
        }).ToList();

        // 2. Подаваме чистия списък на View-то
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

