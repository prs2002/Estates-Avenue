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
        [Authorize(Roles ="manager")]
        [HttpGet("getExecutives")]
        public async Task<IActionResult> GetExecutives()
        {
            var executives = await _userService.GetUsersByTypeAsync("executive");
            return Ok(executives);
        }

        [HttpGet("by-location/{locality}")]
        public async Task<IActionResult> GetUsersByLocation(string locality)
        {
            var executives = await _userService.GetByLocationAsync(locality, "executive");
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
                return BadRequest(new { message = "User already exists" }); // Return JSON object
            }

            // Proceed with user registration
            try
            {
                await _userService.CreateAsync(user);
                return Ok(new { message = "User registered successfully", userId = user.Id }); // Return JSON object
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., to a file or monitoring system)
                Console.Error.WriteLine($"Error during user registration: {ex.Message}");
                return StatusCode(500, new { message = "Internal server error during registration" }); // Return JSON object
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
        [HttpDelete("delete-by-email/{email}")]
        public async Task<IActionResult> DeleteUserByEmail(string email)
        {
            var result = await _userService.FindByEmailAsync(email);
            if (result == null) // Check if the user was found
            {
                return NotFound(); // Return 404 if user not found
            }
            await _userService.DeleteUserAsync(result.Id); // Delete the user by ID
            return NoContent(); // Return 204 No Content after successful deletion
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