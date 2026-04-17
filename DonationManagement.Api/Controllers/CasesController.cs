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
    public class CasesController : ControllerBase
    {
        private readonly ICaseService _caseService;

        public CasesController(ICaseService caseService)
        {
            _caseService = caseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? categoryId)
        {
            var cases = await _caseService.GetAllCasesAsync(categoryId);
            return Ok(cases);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] int? categoryId = null)
        {
            var pagedCases = await _caseService.GetCasesPagedAsync(page, pageSize, categoryId);
            return Ok(pagedCases);
        }

        [HttpGet("kpis")]
        public async Task<IActionResult> GetKpis()
        {
            var kpis = await _caseService.GetCaseKpisAsync();
            return Ok(kpis);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var caseEntity = await _caseService.GetCaseByIdAsync(id);
            if (caseEntity == null) return NotFound();
            return Ok(caseEntity);
        }

        [Authorize(Roles = "Admin,Supervisor")]
        [HttpPost]
        public async Task<IActionResult> Create(CaseRequest request)
        {
            var caseEntity = await _caseService.CreateCaseAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = caseEntity.Id }, caseEntity);
        }

        [Authorize(Roles = "Admin,Supervisor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CaseRequest request)
        {
            var caseEntity = await _caseService.UpdateCaseAsync(id, request);
            if (caseEntity == null) return NotFound();
            return Ok(caseEntity);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _caseService.DeleteCaseAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
