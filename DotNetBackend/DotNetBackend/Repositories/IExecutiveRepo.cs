﻿using DotNetBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBackend.Repositories
{
    public interface IExecutiveRepo
    {
        Task<List<Executive>> GetAllExecutivesAsync();
        Task<Executive> GetExecutiveByIdAsync(string id);
        Task<List<Executive>> GetExecutivesByLocationAsync(string locality);
        Task<Executive> CreateExecutiveAsync(Executive executive);
        Task UpdateExecutiveAsync(string id, Executive executive);
        Task DeleteExecutiveAsync(string id);
    }
}