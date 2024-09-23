namespace DotNetBackend.Repositories
{
    public interface ICustomerRequestRepo
    {
        Task<CustomerRequest> CreateRequestAsync(CustomerRequest request);
        Task<bool> IsPropertyInWishlistAsync(string customerId, string propertyId);
        Task<List<CustomerRequest>> GetCustomerRequestsAsync(string customerId);
        Task<List<CustomerRequest>> GetAllRequestsAsync();
        Task<CustomerRequest> AssignExecutiveToRequestAsync(string requestId, string executiveId);
        Task<List<CustomerRequest>> GetRequestsByExecutiveIdAsync(string executiveId);
        Task<CustomerRequest> UpdateRequestStatusAsync(string requestId, string status);
    }
}
