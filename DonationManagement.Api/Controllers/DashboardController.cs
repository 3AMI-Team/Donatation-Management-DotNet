using DonationManagement.Api.DTOs;
using DonationManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonationManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<ActionResult<DashboardResponse>> GetFullDashboardData()
        {
            var result = await _dashboardService.GetFullDashboardDataAsync();
            return Ok(result);
        }

        [HttpGet("kpis")]
        public async Task<ActionResult<DashboardKpis>> GetKpis()
        {
            var result = await _dashboardService.GetKpisAsync();
            return Ok(result);
        }

        [HttpGet("lastDonations")]
        public async Task<ActionResult<List<RecentDonationResponse>>> GetLastDonations()
        {
            var result = await _dashboardService.GetLastDonationsAsync();
            return Ok(result);
        }

        [HttpGet("lastDistributions")]
        public async Task<ActionResult<List<RecentDistributionResponse>>> GetLastDistributions()
        {
            var result = await _dashboardService.GetLastDistributionsAsync();
            return Ok(result);
        }

        [HttpGet("donationTrends")]
        public async Task<ActionResult<DonationTrends>> GetTrends()
        {
            var result = await _dashboardService.GetTrendsAsync();
            return Ok(result);
        }
    }
}
