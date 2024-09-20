using DotNetBackend.Models;
using DotNetBackend.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNetBackend.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IConfiguration _config;
        public UserService(IUserRepo userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
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

        public string GenerateJwtToken(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Define claims based on user data
            List<Claim> claims = new List<Claim>
            {
                new Claim("Username", user.Email),  // Example claim, could be Username or Email
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // Unique identifier for the token
            };

            // Add role claims
            if (user.UserType == "Manager")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Manager"));
            }
            else if (user.UserType == "Executive")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Executive"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Customer"));
            }

            // Generate the security key from the configuration
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]));

            // Create signing credentials
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Generate the JWT token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),  // Token expiration
                signingCredentials: credentials        // Sign the token
            );

            // Return the generated token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}