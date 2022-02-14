using SureCar.Entities;
using SureCar.Services.Models.UserModels;

namespace SureCar.Services.Interface
{
    public interface IUserService
    {
        Task InitializeBaseAdminAsync();
        Task<bool> CreateUserAsync(User user);
        Task<UserLoginResult?> LoginAsync(UserLogin user);
        Task<bool> LogoutAsync(User user);
        Task<User> GetUserByNameAsync(string userName);
        Task<User> GetUserByIdAsync(string id);
        Task<bool> IsUserAdmin(User user);
        List<string> GetErrors();
    }
}
