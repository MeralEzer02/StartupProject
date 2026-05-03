using Microsoft.AspNetCore.Mvc;
using StartupProject.UI.Services;
using System.Threading.Tasks;

namespace StartupProject.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService;

        public HomeController(UserService userService)
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