using StartupProject.AdminUI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StartupProject.AdminUI.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
            }

            return new ApiResponse<string> { Success = false, Message = "Sunucu ile iletişim kurulamadı." };
        }
    }
}