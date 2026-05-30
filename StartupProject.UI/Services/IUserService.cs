using StartupProject.AdminUI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupProject.AdminUI.Services
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetUsersAsync();
        Task<bool> CreateUserAsync(UserViewModel model);
        Task<bool> UpdateUserAsync(UserViewModel model);
        Task<bool> DeleteUserAsync(Guid id);
    }
}