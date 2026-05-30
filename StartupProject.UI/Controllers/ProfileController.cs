using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StartupProject.AdminUI.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StartupProject.AdminUI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Auth");
            }

            var users = await _userService.GetUsersAsync();
            var currentUser = users?.FirstOrDefault(u => u.Email == userEmail);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(currentUser);
        }
    }
}