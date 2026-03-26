using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeatGamesWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {
        public IActionResult AddGame() => View();


        [Authorize(Roles = "Admin,Developer")]
        public IActionResult UploadPatch() => View();

    }
}
