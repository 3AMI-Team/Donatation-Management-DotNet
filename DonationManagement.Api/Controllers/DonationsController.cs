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
    public class DonationsController : ControllerBase
    {
        private readonly IDonationService _donationService;

        public DonationsController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? categoryId)
        {
            var donations = await _donationService.GetAllDonationsAsync(categoryId);
            return Ok(donations);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] int? categoryId = null)
        {
            var pagedDonations = await _donationService.GetDonationsPagedAsync(page, pageSize, categoryId);
            return Ok(pagedDonations);
        }

        [HttpGet("kpis")]
        public async Task<IActionResult> GetKpis()
        {
            var kpis = await _donationService.GetDonationKpisAsync();
            return Ok(kpis);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null) return NotFound();
            return Ok(donation);
        }

        [Authorize(Roles = "Admin,Supervisor")]
        [HttpPost]
        public async Task<IActionResult> Create(DonationRequest request)
        {
            var donation = await _donationService.CreateDonationAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = donation.Id }, donation);
        }

        [Authorize(Roles = "Admin,Supervisor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DonationRequest request)
        {
            var donation = await _donationService.UpdateDonationAsync(id, request);
            if (donation == null) return NotFound();
            return Ok(donation);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _donationService.DeleteDonationAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
