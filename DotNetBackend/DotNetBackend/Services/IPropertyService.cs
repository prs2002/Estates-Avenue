using DotNetBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBackend.Services
{
    public interface IPropertyService
    {
        Task<List<Property>> GetAllPropertiesAsync();
        Task<Property> GetPropertyByIdAsync(int id);
        Task<string> GetPropertyLocationByIdAsync(int id);
        Task<Property> CreatePropertyAsync(Property property);
        Task UpdatePropertyAsync(int id, Property property);
        Task DeletePropertyAsync(int id);
    }
}