using Microsoft.AspNetCore.Mvc;
using StartupProject.AdminUI.Services;
using System.Threading.Tasks;

namespace StartupProject.AdminUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsersAsync();
            return View(users);
        }
    }
}