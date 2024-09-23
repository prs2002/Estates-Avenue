namespace DotNetBackend.Services
{
    public interface ICustomerRequestService
    {
        Task<string> AddToWishlistAsync(CustomerRequest request);
        Task<CustomerRequest> AddCustomerRequestAsync(CustomerRequest request);
        Task<List<CustomerRequest>> GetCustomerRequestsAsync(string customerId);
        Task<List<CustomerRequest>> GetAllRequestsAsync();
        Task<CustomerRequest> AssignExecutiveToRequestAsync(string requestId, string executiveId);
        Task<List<CustomerRequest>> GetRequestsByExecutiveIdAsync(string executiveId);
        Task<CustomerRequest> UpdateRequestStatusAsync(string requestId, string status);
    }
}