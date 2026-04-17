using Microsoft.AspNetCore.Mvc;
using DonationManagement.Api.DTOs;
using DonationManagement.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            try 
            {
                var result = await _employeeService.LoginAsync(request);
                if (result == null)
                {
                    var allEmployees = await _employeeService.GetAllEmployeesAsync();
                    var usernames = string.Join(", ", allEmployees.Select(e => e.Username));
                    return Unauthorized(new { message = $"Invalid username or password. DB contains users: [{usernames}]" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "Database or Server Error: " + ex.Message, inner = ex.InnerException?.Message });
            }
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

        [HttpGet("diag")]
        public ActionResult GetDiag([FromServices] DonationManagement.Core.Data.DonationDbContext context)
        {
            try
            {
                context.Database.CanConnect();
                var pending = context.Database.GetPendingMigrations();
                return Ok(new { success = true, canConnect = true, pendingMigrations = pending });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, error = ex.ToString() });
            }
        }
    }
}
