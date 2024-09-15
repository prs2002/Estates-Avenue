using DotNetBackend.Models;
using DotNetBackend.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBackend.Services
{
    public class ExecutiveService : IExecutiveService
    {
        private readonly IExecutiveRepo _executiveRepo;

        public ExecutiveService(IExecutiveRepo executiveRepo)
        {
            _executiveRepo = executiveRepo;
        }

        public async Task<List<Executive>> GetAllExecutivesAsync()
        {
            return await _executiveRepo.GetAllExecutivesAsync();
        }

        public async Task<Executive> GetExecutiveByIdAsync(string id)
        {
            return await _executiveRepo.GetExecutiveByIdAsync(id);
        }

        public async Task<List<Executive>> GetExecutivesByLocationAsync(string locality)
        {
            return await _executiveRepo.GetExecutivesByLocationAsync(locality);
        }

        public async Task<Executive> CreateExecutiveAsync(Executive executive)
        {
            return await _executiveRepo.CreateExecutiveAsync(executive);
        }

        public async Task UpdateExecutiveAsync(string id, Executive executive)
        {
            await _executiveRepo.UpdateExecutiveAsync(id, executive);
        }

        public async Task DeleteExecutiveAsync(string id)
        {
            await _executiveRepo.DeleteExecutiveAsync(id);
        }
    }
}