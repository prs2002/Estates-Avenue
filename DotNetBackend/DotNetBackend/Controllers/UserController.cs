using DotNetBackend.Models;
using DotNetBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBackend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("getCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _userService.GetUsersByTypeAsync("customer");
            return Ok(customers);
        }

        [HttpGet("getExecutives")]
        public async Task<IActionResult> GetExecutives()
        {
            var executives = await _userService.GetUsersByTypeAsync("executive");
            return Ok(executives);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("by-location/{locality}")]
        public async Task<IActionResult> GetUsersByLocation(string locality)
        {
            var executives = await _userService.GetByLocationAsync(locality);
            return Ok(executives);
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is null");
            }

            // Check if the user already exists
            var existingUser = await _userService.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return BadRequest("User already exists");
            }

            // Proceed with user registration
            try
            {
                await _userService.CreateAsync(user);
                return Ok("User registered successfully : " + user.Id);
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., to a file or monitoring system)
                Console.Error.WriteLine($"Error during user registration: {ex.Message}");
                return StatusCode(500, "Internal server error during registration");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _userService.LoginAsync(loginRequest.Email, loginRequest.Password);
            if (user != null)
            {
                var token = _userService.GenerateJwtToken(user);
                //return Ok(new { Message = "Login successful", user,token });
                return Ok(new
                {
                    Token = token,
                    User = new
                    {
                        user.Id,
                        user.Email,
                        user.UserType
                    }
                });
            }

            return Unauthorized("Invalid credentials");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user is not null)
            {
                await _userService.DeleteUserAsync(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, User user)
        {
            var iUser = await _userService.GetByIdAsync(id);
            if (iUser is not null)
            {
                await _userService.UpdateUserAsync(id, user);
                return Ok();
            }
            return NotFound();

        }
    }
}