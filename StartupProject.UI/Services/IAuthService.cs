using StartupProject.AdminUI.Models;
using System.Threading.Tasks;

namespace StartupProject.AdminUI.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> LoginAsync(LoginViewModel model);
    }
}