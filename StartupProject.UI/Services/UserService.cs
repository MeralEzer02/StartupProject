using StartupProject.AdminUI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StartupProject.AdminUI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("api/users");

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<UserViewModel>>>();
                if (apiResponse != null && apiResponse.Success)
                {
                    return apiResponse.Data;
                }
            }
            return new List<UserViewModel>();
        }

        public async Task<bool> CreateUserAsync(UserViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserAsync(UserViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/users/{model.Id}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/users/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}