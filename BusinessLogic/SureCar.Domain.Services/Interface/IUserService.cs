using SureCar.Entities;
using SureCar.Services.Models;

namespace SureCar.Services.Interface
{
    public interface IUserService
    {
        Task InitializeBaseAdminAsync();
        Task<bool> CreateUserAsync(User user);
        Task<string> LoginAsync(ApplicationUser user, string password);
        Task LogoutAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByNameAsync(string userName);
        Task<ApplicationUser> GetUserByIdAsync(string id);
    }
}
