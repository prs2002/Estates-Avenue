using DotNetBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Runtime;
using System.Threading.Tasks;

namespace DotNetBackend.Repositories
{
    public class ExecutiveRepo : IExecutiveRepo
    {
        private readonly IMongoCollection<Executive> _executives;
        private readonly REdbSettings _settings;
        public ExecutiveRepo(IOptions<REdbSettings> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _executives = database.GetCollection<Executive>(_settings.ExecCollectionName);
        }

        public async Task<List<Executive>> GetAllExecutivesAsync()
        {
            return await _executives.Find(executive => true).ToListAsync();
        }

        public async Task<Executive> GetExecutiveByIdAsync(string id)
        {
            return await _executives.Find(executive => executive.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Executive>> GetExecutivesByLocationAsync(string locality)
        {
            return await _executives.Find(executive => executive.Location == locality).ToListAsync();
        }

        public async Task<Executive> CreateExecutiveAsync(Executive executive)
        {
            await _executives.InsertOneAsync(executive);
            return executive;
        }

        public async Task UpdateExecutiveAsync(string id, Executive executive)
        {
            await _executives.ReplaceOneAsync(e => e.Id == id, executive);
        }

        public async Task DeleteExecutiveAsync(string id)
        {
            await _executives.DeleteOneAsync(executive => executive.Id == id);
        }
    }
}