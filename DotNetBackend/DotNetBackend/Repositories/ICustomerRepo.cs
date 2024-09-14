using DotNetBackend.Models;

namespace DotNetBackend.Repositories
{
    public interface ICustomerRepo
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(string id);
        Task<Customer> CreateAsync(Customer customer);
        Task UpdateCustomerAsync(string id, Customer customer);
        Task DeleteCustomerAsync(string id);
        Task<Customer> FindByEmailAsync(string email);
    }
}
