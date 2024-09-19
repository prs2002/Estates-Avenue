using DotNetBackend.Models;
using DotNetBackend.Repositories;

namespace DotNetBackend.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public Task<User> CreateAsync(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return _userRepo.CreateUserAsync(user);
        }

        public Task DeleteUserAsync(string id)
        {
            return _userRepo.DeleteUserAsync(id);
        }

        public Task<List<User>> GetAllAsync()
        {
            return _userRepo.GetAllUsersAsync();
        }

        public Task<User> GetByIdAsync(string id)
        {
            return _userRepo.GetUserByIdAsync(id);
        }
        public async Task<List<User>> GetByLocationAsync(string locality)
        {
            return await _userRepo.GetUserByLocationAsync(locality);
        }
        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _userRepo.FindByEmailAsync(email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }
            return null;
        }

        public Task UpdateUserAsync(string id, User user)
        {
            return _userRepo.UpdateUserAsync(id, user);
        }

        Task<User> IUserService.FindByEmailAsync(string email)
        {
            return _userRepo.FindByEmailAsync(email);
        }
    }
}