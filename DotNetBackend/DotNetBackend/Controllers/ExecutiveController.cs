using DotNetBackend.Models;
using DotNetBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExecutiveController : ControllerBase
    {
        private readonly IExecutiveService _executiveService;

        public ExecutiveController(IExecutiveService executiveService)
        {
            _executiveService = executiveService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExecutives()
        {
            var executives = await _executiveService.GetAllExecutivesAsync();
            return Ok(executives);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExecutiveById(string id)
        {
            var executive = await _executiveService.GetExecutiveByIdAsync(id);
            if (executive == null) return NotFound();
            return Ok(executive);
        }

        [HttpGet("by-location/{locality}")]
        public async Task<IActionResult> GetExecutivesByLocation(string locality)
        {
            var executives = await _executiveService.GetExecutivesByLocationAsync(locality);
            return Ok(executives);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExecutive([FromBody] Executive executive)
        {
            var createdExecutive = await _executiveService.CreateExecutiveAsync(executive);
            return CreatedAtAction(nameof(GetExecutiveById), new { id = createdExecutive.Id }, createdExecutive);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExecutive(string id, [FromBody] Executive executive)
        {
            await _executiveService.UpdateExecutiveAsync(id, executive);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExecutive(string id)
        {
            await _executiveService.DeleteExecutiveAsync(id);
            return NoContent();
        }
    }
}