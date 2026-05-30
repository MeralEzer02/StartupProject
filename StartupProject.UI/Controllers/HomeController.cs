using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StartupProject.AdminUI.Models;
using StartupProject.AdminUI.Services;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject.AdminUI.Controllers
{
    [Authorize]
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

            bool isApiOnline = users != null;

            var dashboardData = new DashboardViewModel
            {
                UserCount = isApiOnline ? users.Count : 0,
                RoleCount = isApiOnline ? users.Where(u => !string.IsNullOrEmpty(u.Role)).Select(u => u.Role).Distinct().Count() : 0,
                SystemStatus = isApiOnline ? "Aktif (API Baðlý)" : "Baðlantý Hatasý",
                Users = users
            };

            return View(dashboardData);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}