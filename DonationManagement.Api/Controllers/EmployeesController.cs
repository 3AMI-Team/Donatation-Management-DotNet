using Microsoft.AspNetCore.Mvc;
using DonationManagement.Api.DTOs;
using DonationManagement.Api.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;

namespace DonationManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetAllEmployees()
        {
            var result = await _employeeService.GetAllEmployeesAsync();
            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PaginatedResponse<EmployeeResponse>>> GetEmployeesPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _employeeService.GetEmployeesPagedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeById(int id)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeResponse>> CreateEmployee(EmployeeRequest request)
        {
            var result = await _employeeService.CreateEmployeeAsync(request);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeResponse>> UpdateEmployee(int id, EmployeeRequest request)
        {
            var result = await _employeeService.UpdateEmployeeAsync(id, request);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}/registered-cases")]
        public async Task<ActionResult<IEnumerable<CaseResponse>>> GetRegisteredCases(int id)
        {
            var cases = await _employeeService.GetRegisteredCasesAsync(id);
            return Ok(cases);
        }

        [HttpGet("{id}/handled-distributions")]
        public async Task<ActionResult<IEnumerable<DistributionResponse>>> GetHandledDistributions(int id)
        {
            var distributions = await _employeeService.GetHandledDistributionsAsync(id);
            return Ok(distributions);
        }

        // Employee management endpoints for donors
        [HttpPost("donors")]
        public async Task<ActionResult<DonorResponse>> CreateDonor(DonorRequest request)
        {
            var result = await _employeeService.CreateDonorAsync(request);
            return CreatedAtAction(nameof(CreateDonor), new { id = result.Id }, result);
        }

        [HttpDelete("donors/{id}")]
        public async Task<IActionResult> DeleteDonor(int id)
        {
            var deleted = await _employeeService.DeleteDonorAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // Employee management endpoints for categories
        [HttpPost("categories")]
        public async Task<ActionResult<CategoryResponse>> CreateCategory(CategoryRequest request)
        {
            var result = await _employeeService.CreateCategoryAsync(request);
            return CreatedAtAction(nameof(CreateCategory), new { id = result.Id }, result);
        }

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await _employeeService.DeleteCategoryAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // Employee management endpoints for cases
        [HttpPost("cases")]
        public async Task<ActionResult<CaseResponse>> CreateCase(CaseRequest request)
        {
            var result = await _employeeService.CreateCaseAsync(request);
            return CreatedAtAction(nameof(CreateCase), new { id = result.Id }, result);
        }

        [HttpDelete("cases/{id}")]
        public async Task<IActionResult> DeleteCase(int id)
        {
            var deleted = await _employeeService.DeleteCaseAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
