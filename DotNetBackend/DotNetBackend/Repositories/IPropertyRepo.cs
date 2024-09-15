using DotNetBackend.Models;

namespace DotNetBackend.Repositories
{
    public interface IPropertyRepo
    {
        Task<List<Property>> GetAllAsync();
        Task<Property> GetByIdAsync(int id);
        Task<string> GetPropertyLocationByIdAsync(int id);
        Task<Property> CreateAsync(Property property);
        Task UpdatePropertyAsync(int id, Property property);
        Task DeletePropertyAsync(int id);
    }
}