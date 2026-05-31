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
        public IActionResult Create(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl ?? Request.Headers["Referer"].ToString();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model, string returnUrl = null)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                var isSuccess = await _userService.CreateUserAsync(model);
                if (isSuccess)
                {
                    return Redirect(returnUrl ?? Url.Action("Index"));
                }
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, string returnUrl = null)
        {
            var users = await _userService.GetUsersAsync();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return RedirectToAction(nameof(Index));

            ViewBag.ReturnUrl = returnUrl ?? Request.Headers["Referer"].ToString();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model, string returnUrl = null)
        {
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                var isSuccess = await _userService.UpdateUserAsync(model);
                if (isSuccess)
                {
                    return Redirect(returnUrl ?? Url.Action("Index"));
                }
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}