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
            // 1. Email adresine göre kullanıcıyı bul
            // Not: İleride Repository pattern'e GetByExpression gibi özel metotlar ekleyerek bu sorguyu optimize edeceğiz.
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == loginDto.Email);

            // 2. Kullanıcı sistemde yoksa genel bir hata dön (Güvenlik gereği "Kullanıcı yok" yerine yuvarlak bir mesaj veririz)
            if (user == null)
            {
                return Ok(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Email veya şifre hatalı.",
                    Data = null
                });
            }

            // 3. BCrypt ile şifre doğrulama (Gelen düz metin şifreyi, DB'deki Hash ile matematiksel olarak kıyaslar)
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return Ok(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Email veya şifre hatalı.",
                    Data = null
                });
            }

            // 4. Doğrulama başarılıysa onay dön. 
            // (UI tarafında bu onayı aldığımızda gerçek Cookie oturumunu başlatacağız)
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Giriş başarılı.",
                Data = user.Id.ToString() // Şimdilik sadece Id dönüyoruz, yetki işlemlerinde bu değişecek
            });
        }
    }
}