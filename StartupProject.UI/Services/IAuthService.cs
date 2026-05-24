using StartupProject.AdminUI.Models;
using System.Threading.Tasks;

namespace StartupProject.AdminUI.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResponseViewModel>> LoginAsync(LoginViewModel model);
    }
}