using Microsoft.AspNetCore.Mvc;
using DonationManagement.Api.DTOs;
using DonationManagement.Api.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;

namespace DonationManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly ICaseService _caseService;

        public CasesController(ICaseService caseService)
        {
            _caseService = caseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseResponse>>> GetAllCases()
        {
            var result = await _caseService.GetAllCasesAsync();
            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PaginatedResponse<CaseResponse>>> GetCasesPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _caseService.GetCasesPagedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CaseResponse>> GetCaseById(int id)
        {
            var result = await _caseService.GetCaseByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CaseResponse>> CreateCase(CaseRequest request)
        {
            var result = await _caseService.CreateCaseAsync(request);
            return CreatedAtAction(nameof(GetCaseById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CaseResponse>> UpdateCase(int id, CaseRequest request)
        {
            var result = await _caseService.UpdateCaseAsync(id, request);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCase(int id)
        {
            var deleted = await _caseService.DeleteCaseAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}/distributions")]
        public async Task<ActionResult<IEnumerable<DistributionResponse>>> GetCaseDistributions(int id)
        {
            var distributions = await _caseService.GetCaseDistributionsAsync(id);
            return Ok(distributions);
        }

        [HttpGet("{id}/remaining")]
        public async Task<ActionResult<decimal>> GetRemainingAmountNeeded(int id)
        {
            var remaining = await _caseService.GetRemainingAmountNeededAsync(id);
            return Ok(remaining);
        }

        [HttpGet("{id}/isfunded")]
        public async Task<ActionResult<bool>> IsFullyFunded(int id)
        {
            var funded = await _caseService.IsFullyFundedAsync(id);
            return Ok(funded);
        }
    }
}
