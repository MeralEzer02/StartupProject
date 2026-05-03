using StartupProject.UI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StartupProject.UI.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7095/api/users");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserViewModel>>();
            }

            return new List<UserViewModel>();
        }
    }
}