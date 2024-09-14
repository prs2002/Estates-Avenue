using DotNetBackend.Models;
using DotNetBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace DotNetBackend.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is null");
            }

            // Check if the customer already exists
            var existingCustomer = await _customerService.FindByEmailAsync(customer.Email);
            if (existingCustomer != null)
            {
                return BadRequest("Customer already exists");
            }

            // Proceed with customer registration
            try
            {
                await _customerService.CreateAsync(customer);
                return Ok("Customer registered successfully : " + customer.Cid);
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., to a file or monitoring system)
                Console.Error.WriteLine($"Error during customer registration: {ex.Message}");
                return StatusCode(500, "Internal server error during registration");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            Console.WriteLine($"Received login request for email: {loginRequest.Email}");

            var customer = await _customerService.LoginAsync(loginRequest.Email, loginRequest.Password);
            if (customer != null)
            {
                Console.WriteLine($"User found: {customer.Email}");

                if (customer.Cid != null)
                {
                    HttpContext.Session.SetString("UserId", customer.Cid);
                    HttpContext.Session.SetString("UserEmail", customer.Email);

                    Console.WriteLine("Login successful");
                    return Ok(new { Message = "Login successful", User = customer });
                }
                else
                {
                    Console.WriteLine("User ID is null");
                    return BadRequest("User ID cannot be null");
                }
            }

            Console.WriteLine("Invalid credentials");
            return Unauthorized("Invalid credentials");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Clear session on logout
            HttpContext.Session.Clear();
            return Ok("Logged out");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer is not null)
            {
                await _customerService.DeleteCustomerAsync(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, Customer customer)
        {
            var iCustomer = await _customerService.GetByIdAsync(id);
            if (iCustomer is not null)
            {
                await _customerService.UpdateCustomerAsync(id, customer);
                return Ok();
            }
            return NotFound();

        }



    }
}
