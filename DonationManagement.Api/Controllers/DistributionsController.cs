using DonationManagement.Api.DTOs;
using DonationManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DonationManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DistributionsController : ControllerBase
    {
        private readonly IDistributionService _distributionService;

        public DistributionsController(IDistributionService distributionService)
        {
            _distributionService = distributionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var distributions = await _distributionService.GetAllDistributionsAsync();
            return Ok(distributions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var distribution = await _distributionService.GetDistributionByIdAsync(id);
            if (distribution == null) return NotFound();
            return Ok(distribution);
        }

        [HttpGet("kpis")]
        public async Task<IActionResult> GetKpis()
        {
            var kpis = await _distributionService.GetDistributionKpisAsync();
            return Ok(kpis);
        }

        [Authorize(Roles = "Admin,Supervisor")]
        [HttpPost]
        public async Task<IActionResult> Create(DistributionRequest request)
        {
            var distribution = await _distributionService.CreateDistributionAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = distribution.Id }, distribution);
        }

        [Authorize(Roles = "Admin,Supervisor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DistributionRequest request)
        {
            var distribution = await _distributionService.UpdateDistributionAsync(id, request);
            if (distribution == null) return NotFound();
            return Ok(distribution);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _distributionService.DeleteDistributionAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
