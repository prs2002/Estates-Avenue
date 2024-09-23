using DotNetBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotNetBackend.Repositories
{
    public class UserRepo: IUserRepo
    {

        private readonly IMongoCollection<User> _users;
        private readonly REdbSettings _settings;
        public UserRepo(IOptions<REdbSettings> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _users = database.GetCollection<User>(_settings.UserCollection);
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }
        public async Task<List<User>> GetUsersByTypeAsync(string userType)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserType, userType);
            return await _users.Find(filter).ToListAsync();
        }
        public async Task<List<User>> GetUserByLocationAsync(string locality, string userType)
        {
            return await _users
                .Find(user => user.Location == locality && user.UserType == userType)
                .ToListAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task UpdateUserAsync(string id, User user)
        {
            await _users.ReplaceOneAsync(e => e.Id == id, user);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _users.DeleteOneAsync(executive => executive.Id == id);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _users.Find(c => c.Email == email).FirstOrDefaultAsync();
        }
    }
}
