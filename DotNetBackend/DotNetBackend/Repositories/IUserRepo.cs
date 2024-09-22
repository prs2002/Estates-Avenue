using DotNetBackend.Models;

namespace DotNetBackend.Repositories
{
    public interface IUserRepo
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        Task<List<User>> GetUsersByTypeAsync(string userType);
        Task<List<User>> GetUserByLocationAsync(string locality);
        Task<User> CreateUserAsync(User executive);
        Task UpdateUserAsync(string id, User executive);
        Task DeleteUserAsync(string id);
        Task<User> FindByEmailAsync(string email);

    }
}
