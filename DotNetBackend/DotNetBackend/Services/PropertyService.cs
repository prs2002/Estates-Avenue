using DotNetBackend.Models;
using DotNetBackend.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBackend.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepo _propertyRepo;

        public PropertyService(IPropertyRepo propertyRepo)
        {
            _propertyRepo = propertyRepo;
        }

        public async Task<List<Property>> GetAllPropertiesAsync()
        {
            return await _propertyRepo.GetAllAsync();
        }

        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            return await _propertyRepo.GetByIdAsync(id);
        }

        public async Task<string> GetPropertyLocationByIdAsync(int id)
        {
            return await _propertyRepo.GetPropertyLocationByIdAsync(id);
        }

        public async Task<Property> CreatePropertyAsync(Property property)
        {
            return await _propertyRepo.CreateAsync(property);
        }

        public async Task UpdatePropertyAsync(int id, Property property)
        {
            await _propertyRepo.UpdatePropertyAsync(id, property);
        }

        public async Task DeletePropertyAsync(int id)
        {
            await _propertyRepo.DeletePropertyAsync(id);
        }
    }
}