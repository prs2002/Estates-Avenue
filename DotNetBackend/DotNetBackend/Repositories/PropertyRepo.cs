using DotNetBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetBackend.Repositories
{
    public class PropertyRepo : IPropertyRepo
    {
        private readonly AppDbContext _context;

        public PropertyRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Property>> GetAllAsync()
        {
            return await _context.Property.ToListAsync();
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            return await _context.Property.FindAsync(id);
        }

        public async Task<string> GetPropertyLocationByIdAsync(int id)
        {
            var property = await _context.Property.FindAsync(id);
            return property?.Location;
        }

        public async Task<Property> CreateAsync(Property property)
        {
            _context.Property.Add(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task UpdatePropertyAsync(int id, Property property)
        {
            var existingProperty = await _context.Property.FindAsync(id);
            if (existingProperty != null)
            {
                _context.Entry(existingProperty).CurrentValues.SetValues(property);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePropertyAsync(int id)
        {
            var property = await _context.Property.FindAsync(id);
            if (property != null)
            {
                _context.Property.Remove(property);
                await _context.SaveChangesAsync();
            }
        }
    }
}
