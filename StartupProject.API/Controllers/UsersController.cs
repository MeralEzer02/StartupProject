using Microsoft.AspNetCore.Mvc;
using StartupProject.Data;
using System.Linq;

namespace StartupProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            _context.Users.Add(user);

            _context.SaveChanges();

            return Ok(user);
        }
    }
}