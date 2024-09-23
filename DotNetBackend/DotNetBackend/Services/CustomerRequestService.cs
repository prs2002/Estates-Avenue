using DotNetBackend.Repositories;

namespace DotNetBackend.Services
{
    public class CustomerRequestService : ICustomerRequestService
    {
        private readonly ICustomerRequestRepo _customerRequestRepo;

        public CustomerRequestService(ICustomerRequestRepo customerRequestRepo)
        {
            _customerRequestRepo = customerRequestRepo;
        }
        public async Task<string> AddToWishlistAsync(CustomerRequest request)
        {
            // Check if the property is already in the wishlist
            bool isInWishlist = await _customerRequestRepo.IsPropertyInWishlistAsync(request.CustomerId, request.PropertyId);

            if (isInWishlist)
            {
                return "Property already exists in the wishlist.";
            }

            // If the property is not in the wishlist, add it
            await _customerRequestRepo.CreateRequestAsync(request);
            return "Property added to wishlist successfully.";
        }
        public async Task<CustomerRequest> AddCustomerRequestAsync(CustomerRequest request)
        {
            return await _customerRequestRepo.CreateRequestAsync(request);
        }

        public async Task<List<CustomerRequest>> GetCustomerRequestsAsync(string customerId)
        {
            return await _customerRequestRepo.GetCustomerRequestsAsync(customerId);
        }

        public async Task<List<CustomerRequest>> GetAllRequestsAsync()
        {
            return await _customerRequestRepo.GetAllRequestsAsync();
        }

        public async Task<CustomerRequest> AssignExecutiveToRequestAsync(string requestId, string executiveId)
        {
            return await _customerRequestRepo.AssignExecutiveToRequestAsync(requestId, executiveId);
        }

        public async Task<List<CustomerRequest>> GetRequestsByExecutiveIdAsync(string executiveId)
        {
            return await _customerRequestRepo.GetRequestsByExecutiveIdAsync(executiveId);
        }

        public async Task<CustomerRequest> UpdateRequestStatusAsync(string requestId, string status)
        {
            return await _customerRequestRepo.UpdateRequestStatusAsync(requestId, status);
        }
    }
}
