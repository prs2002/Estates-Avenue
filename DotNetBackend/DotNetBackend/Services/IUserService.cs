using DotNetBackend.Models;

namespace DotNetBackend.Services
{
    public interface IUserService
    {
        Task<User?> LoginAsync(string email, string password);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task<List<User>> GetByLocationAsync(string locality);  // New method to get executives by locality
        Task<User> CreateAsync(User user);
        Task DeleteUserAsync(string id);
        Task UpdateUserAsync(string id, User user);
        Task<User> FindByEmailAsync(string email);
        string GenerateJwtToken(User user);

    }
}
