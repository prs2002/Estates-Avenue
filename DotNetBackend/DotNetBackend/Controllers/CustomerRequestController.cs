using DotNetBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerRequestController : ControllerBase
    {
        private readonly ICustomerRequestService _customerRequestService;

        public CustomerRequestController(ICustomerRequestService customerRequestService)
        {
            _customerRequestService = customerRequestService;
        }

        //[HttpPost]
        //public async Task<IActionResult> AddRequest([FromBody] CustomerRequest request)
        //{
        //    var createdRequest = await _customerRequestService.AddCustomerRequestAsync(request);
        //  return Ok(createdRequest);
        //}
        [HttpPost]
        public async Task<IActionResult> AddToWishlist([FromBody] CustomerRequest request)
        {
            // Call the service to add the property to the wishlist
            var result = await _customerRequestService.AddToWishlistAsync(request);

            if (result == "Property already exists in the wishlist.")
            {
                return Conflict(new { message = result });
            }

            return Ok(new { message = result });
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerRequests(string customerId)
        {
            var requests = await _customerRequestService.GetCustomerRequestsAsync(customerId);
            return Ok(requests);
        }

        [HttpGet("manager")]
        public async Task<IActionResult> GetAllRequests()
        {
            var requests = await _customerRequestService.GetAllRequestsAsync();
            return Ok(requests);
        }

        [HttpPost("{requestId}/assign-executive/{executiveId}")]
        public async Task<IActionResult> AssignExecutive(string requestId, string executiveId)
        {
            var updatedRequest = await _customerRequestService.AssignExecutiveToRequestAsync(requestId, executiveId);
            return Ok(updatedRequest);
        }

        [HttpGet("executive/{executiveId}")]
        public async Task<IActionResult> GetRequestsForExecutive(string executiveId)
        {
            var requests = await _customerRequestService.GetRequestsByExecutiveIdAsync(executiveId);
            return Ok(requests);
        }

        [HttpPatch("{requestId}/status")]
        public async Task<IActionResult> UpdateRequestStatus(string requestId, [FromBody] string status)
        {
            var updatedRequest = await _customerRequestService.UpdateRequestStatusAsync(requestId, status);
            return Ok(updatedRequest);
        }
    }
}
