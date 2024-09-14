using DotNetBackend.Models;
using DotNetBackend.Repositories;

namespace DotNetBackend.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public Task<Customer> CreateAsync(Customer customer)
        {
            customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);
            return _customerRepo.CreateAsync(customer);
        }

        public Task DeleteCustomerAsync(string id)
        {
            return _customerRepo.DeleteCustomerAsync(id);
        }

        public Task<List<Customer>> GetAllAsync()
        {
            return _customerRepo.GetAllAsync();
        }

        public Task<Customer> GetByIdAsync(string id)
        {
            return _customerRepo.GetByIdAsync(id);
        }

        public async Task<Customer?> LoginAsync(string email, string password)
        {
            var customer = await _customerRepo.FindByEmailAsync(email);
            if (customer != null && BCrypt.Net.BCrypt.Verify(password, customer.Password))
            {
                return customer;
            }
            return null;
        }

        public Task UpdateCustomerAsync(string id, Customer customer)
        {
            return _customerRepo.UpdateCustomerAsync(id, customer);
        }

        Task<Customer> ICustomerService.FindByEmailAsync(string email)
        {
            return _customerRepo.FindByEmailAsync(email);
        }
    }
}
