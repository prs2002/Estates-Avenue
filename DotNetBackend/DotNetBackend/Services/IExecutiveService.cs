using DotNetBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBackend.Services
{
    public interface IExecutiveService
    {
        Task<List<Executive>> GetAllExecutivesAsync();
        Task<Executive> GetExecutiveByIdAsync(string id);
        Task<List<Executive>> GetExecutivesByLocationAsync(string locality);  // New method to get executives by locality
        Task<Executive> CreateExecutiveAsync(Executive executive);
        Task UpdateExecutiveAsync(string id, Executive executive);
        Task DeleteExecutiveAsync(string id);
    }
}