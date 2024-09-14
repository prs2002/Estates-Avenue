using DotNetBackend.Models;

namespace DotNetBackend.Services
{
    public interface ICustomerService
    {
        Task<Customer?> LoginAsync(string email, string password);
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(string id);
        Task<Customer> CreateAsync(Customer customer);
        Task DeleteCustomerAsync(string id);
        Task UpdateCustomerAsync(string id, Customer customer);
        Task<Customer> FindByEmailAsync(string email);
    }
}
