using StartupProject.UI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupProject.UI.Services
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetUsersAsync();
    }
}