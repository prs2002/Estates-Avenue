using DotNetBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotNetBackend.Repositories
{
    public class CustomerRepo: ICustomerRepo
    {
        private readonly IMongoCollection<Customer> _customerCollection;
        private readonly REdbSettings _settings;
        public CustomerRepo(IOptions<REdbSettings> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _customerCollection = database.GetCollection<Customer>(_settings.CustCollection);
        }
        public Task<List<Customer>> GetAllAsync()
        {
            return _customerCollection.Find(c => true).ToListAsync();
        }

        public Task<Customer> GetByIdAsync(string id)
        {
            return _customerCollection.Find(c => c.Cid == id).FirstOrDefaultAsync();
        }
        public async Task<Customer> CreateAsync(Customer customer)
        {
            await _customerCollection.InsertOneAsync(customer);
            return customer;
        }

        public async Task UpdateCustomerAsync(string id, Customer customer)
        {
            await _customerCollection.ReplaceOneAsync(c => c.Cid == id, customer);
        }
        public async Task DeleteCustomerAsync(string id)
        {
            await _customerCollection.DeleteOneAsync(c => c.Cid == id);
        }

        public async Task<Customer> FindByEmailAsync(string email)
        {
            return await _customerCollection.Find(c => c.Email == email).FirstOrDefaultAsync();
        }
}
}
