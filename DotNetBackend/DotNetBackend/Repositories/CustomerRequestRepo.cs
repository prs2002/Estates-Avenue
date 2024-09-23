using DotNetBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotNetBackend.Repositories
{
    public class CustomerRequestRepo : ICustomerRequestRepo
    {
        private readonly IMongoCollection<CustomerRequest> _customerRequests;
        private readonly REdbSettings _settings;
        public CustomerRequestRepo(IOptions<REdbSettings> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _customerRequests = database.GetCollection<CustomerRequest>(_settings.CustReqCollection);
        }
        public async Task<bool> IsPropertyInWishlistAsync(string customerId, string propertyId)
        {
            var existingRequest = await _customerRequests.Find(r =>
                r.CustomerId == customerId && r.PropertyId == propertyId).FirstOrDefaultAsync();

            return existingRequest != null; // Returns true if the property is already in the wishlist
        }
        public async Task<CustomerRequest> CreateRequestAsync(CustomerRequest request)
        {
            await _customerRequests.InsertOneAsync(request);
            return request;
        }
        public async Task<List<CustomerRequest>> GetCustomerRequestsAsync(string customerId)
        {
            return await _customerRequests.Find(r => r.CustomerId == customerId).ToListAsync();
        }
        public async Task<List<CustomerRequest>> GetAllRequestsAsync()
        {
            return await _customerRequests.Find(customerRequest => true).ToListAsync();
        }
        public async Task<CustomerRequest> AssignExecutiveToRequestAsync(string requestId, string executiveId)
        {
            var update = Builders<CustomerRequest>.Update.Set(r => r.ExecutiveId, executiveId).Set(r => r.RequestStatus, "fulfilled"); ;
            return await _customerRequests.FindOneAndUpdateAsync(r => r.Rid == requestId, update);
        }
        public async Task<List<CustomerRequest>> GetRequestsByExecutiveIdAsync(string executiveId)
        {
            return await _customerRequests.Find(r => r.ExecutiveId == executiveId).ToListAsync();
        }
        public async Task<CustomerRequest> UpdateRequestStatusAsync(string requestId, string status)
        {
            var update = Builders<CustomerRequest>.Update.Set(r => r.RequestStatus, status);
            return await _customerRequests.FindOneAndUpdateAsync(r => r.Rid == requestId, update);
        }
    }
}