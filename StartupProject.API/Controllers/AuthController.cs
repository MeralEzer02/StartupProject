using Microsoft.AspNetCore.Mvc;
using StartupProject.Data;
using StartupProject.Data.DTOs;
using StartupProject.Data.Repositories;
using System.Linq;

namespace StartupProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public AuthController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return Ok(new ApiResponse<LoginResponseDto> { Success = false, Message = "Email veya şifre hatalı." });
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return Ok(new ApiResponse<LoginResponseDto> { Success = false, Message = "Email veya şifre hatalı." });
            }

            var responseDto = new LoginResponseDto
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Role = user.Role
            };

            return Ok(new ApiResponse<LoginResponseDto>
            {
                Success = true,
                Message = "Giriş başarılı.",
                Data = responseDto
            });
        }
    }
}