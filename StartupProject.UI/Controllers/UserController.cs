using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StartupProject.AdminUI.Models;
using StartupProject.AdminUI.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject.AdminUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsersAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _userService.CreateUserAsync(model);
                if (isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var users = await _userService.GetUsersAsync();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null) return RedirectToAction(nameof(Index));

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _userService.UpdateUserAsync(model);
                if (isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isSuccess = await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}