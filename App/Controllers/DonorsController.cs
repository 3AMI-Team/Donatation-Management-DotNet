using Microsoft.AspNetCore.Mvc;
using DonationManagement.Api.DTOs;
using DonationManagement.Api.Services;

namespace DonationManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorsController : ControllerBase
    {
        private readonly IDonorService _donorService;

        public DonorsController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonorResponse>>> GetAllDonors()
        {
            var result = await _donorService.GetAllDonorsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DonorResponse>> GetDonorById(int id)
        {
            var result = await _donorService.GetDonorByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DonorResponse>> CreateDonor(DonorRequest request)
        {
            var result = await _donorService.CreateDonorAsync(request);
            return CreatedAtAction(nameof(GetDonorById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DonorResponse>> UpdateDonor(int id, DonorRequest request)
        {
            var result = await _donorService.UpdateDonorAsync(id, request);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonor(int id)
        {
            var deleted = await _donorService.DeleteDonorAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}/cases")]
        public async Task<ActionResult<IEnumerable<CaseResponse>>> GetDonorCases(int id)
        {
            var cases = await _donorService.GetDonorCasesAsync(id);
            return Ok(cases);
        }
    }
}
