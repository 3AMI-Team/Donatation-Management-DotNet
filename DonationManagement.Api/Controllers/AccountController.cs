using Microsoft.AspNetCore.Mvc;
using DonationManagement.Api.DTOs;
using DonationManagement.Api.Services.Interfaces;

namespace DonationManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDonorService _donorService;

        public AccountController(IEmployeeService employeeService, IDonorService donorService)
        {
            _employeeService = employeeService;
            _donorService = donorService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            var result = await _employeeService.LoginAsync(request);
            if (result == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            return Ok(result);
        }

        [HttpPost("donor-login")]
        public async Task<ActionResult<AuthResponse>> DonorLogin(DonorLoginRequest request)
        {
            var result = await _donorService.LoginAsync(request);
            if (result == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(result);
        }

        [HttpPost("donor-signup")]
        public async Task<ActionResult<DonorResponse>> DonorSignup(DonorSignupRequest request)
        {
            var result = await _donorService.SignupAsync(request);
            return Ok(result);
        }
    }
}
