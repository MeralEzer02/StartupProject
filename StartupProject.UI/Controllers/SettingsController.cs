using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StartupProject.AdminUI.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ApplicationName"] = "StartupProject Admin Panel";
            ViewData["Environment"] = "Development (Geliştirme Ortamı)";
            ViewData["Version"] = "v1.0.0";
            ViewData["Database"] = "Microsoft SQL Server (LocalDB) - EF Core";

            return View();
        }
    }
}