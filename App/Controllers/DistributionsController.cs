using Microsoft.AspNetCore.Mvc;
using DonationManagement.Api.DTOs;
using DonationManagement.Api.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;

namespace DonationManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DistributionsController : ControllerBase
    {
        private readonly IDistributionService _distributionService;

        public DistributionsController(IDistributionService distributionService)
        {
            _distributionService = distributionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistributionResponse>>> GetAllDistributions()
        {
            var result = await _distributionService.GetAllDistributionsAsync();
            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PaginatedResponse<DistributionResponse>>> GetDistributionsPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _distributionService.GetDistributionsPagedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DistributionResponse>> GetDistributionById(int id)
        {
            var result = await _distributionService.GetDistributionByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DistributionResponse>> CreateDistribution(DistributionRequest request)
        {
            var result = await _distributionService.CreateDistributionAsync(request);
            return CreatedAtAction(nameof(GetDistributionById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DistributionResponse>> UpdateDistribution(int id, DistributionRequest request)
        {
            var result = await _distributionService.UpdateDistributionAsync(id, request);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistribution(int id)
        {
            var deleted = await _distributionService.DeleteDistributionAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("bycase/{caseId}")]
        public async Task<ActionResult<IEnumerable<DistributionResponse>>> GetDistributionsByCase(int caseId)
        {
            var distributions = await _distributionService.GetDistributionsByCaseAsync(caseId);
            return Ok(distributions);
        }

        [HttpPost("even")]
        public async Task<ActionResult<DistributionResponse[]>> DistributeEvenly(EvenDistributionRequest request)
        {
            var distributions = await _distributionService.DistributeEvenlyAsync(request);
            return Ok(distributions);
        }
    }
}
