using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace StartupProject.AdminUI.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            var roles = new List<string>
            {
                "SuperAdmin",
                "Admin",
                "User"
            };

            return View(roles);
        }
    }
}