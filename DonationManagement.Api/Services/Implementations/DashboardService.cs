using DonationManagement.Api.DTOs;
using DonationManagement.Api.Services.Interfaces;
using DonationManagement.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonationManagement.Api.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly DonationDbContext _context;

        public DashboardService(DonationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardResponse> GetFullDashboardDataAsync()
        {
            return new DashboardResponse
            {
                Kpis = await GetKpisAsync(),
                LastDonations = await GetLastDonationsAsync(),
                LastDistributions = await GetLastDistributionsAsync(),
                Trends = await GetTrendsAsync()
            };
        }

        public async Task<DashboardKpis> GetKpisAsync()
        {
            var now = DateTime.UtcNow;
            var startOfThisMonth = new DateTime(now.Year, now.Month, 1);
            var startOfLastMonth = startOfThisMonth.AddMonths(-1);
            var endOfLastMonth = startOfThisMonth.AddTicks(-1);

            // Total Donations
            var totalDonationsAmount = await _context.Cases.SumAsync(c => c.Amount);
            var thisMonthDonations = await _context.Cases.Where(c => c.Date >= startOfThisMonth).SumAsync(c => c.Amount);
            var lastMonthDonations = await _context.Cases.Where(c => c.Date >= startOfLastMonth && c.Date <= endOfLastMonth).SumAsync(c => c.Amount);

            // Active Cases
            var activeCasesCount = await _context.Cases.CountAsync(c => c.Status != "Closed");
            var thisMonthActive = await _context.Cases.CountAsync(c => c.Date >= startOfThisMonth && c.Status != "Closed");
            var lastMonthActive = await _context.Cases.CountAsync(c => c.Date >= startOfLastMonth && c.Date <= endOfLastMonth && c.Status != "Closed");

            // Total Donors
            var totalDonorsCount = await _context.Donors.CountAsync();
            var thisMonthDonors = await _context.Donors.CountAsync(d => d.RegisterDate >= startOfThisMonth);
            var lastMonthDonors = await _context.Donors.CountAsync(d => d.RegisterDate >= startOfLastMonth && d.RegisterDate <= endOfLastMonth);

            // Funds Distributed
            var fundsDistributedAmount = await _context.Distributions.SumAsync(d => d.Amount);
            var thisMonthDist = await _context.Distributions.Where(d => d.DistributionDate >= startOfThisMonth).SumAsync(d => d.Amount);
            var lastMonthDist = await _context.Distributions.Where(d => d.DistributionDate >= startOfLastMonth && d.DistributionDate <= endOfLastMonth).SumAsync(d => d.Amount);

            return new DashboardKpis
            {
                TotalDonations = new KpiItem { Amount = totalDonationsAmount, VsLastMonth = CalculateTrend(thisMonthDonations, lastMonthDonations) },
                ActiveCases = new KpiItem { Amount = activeCasesCount, VsLastMonth = CalculateTrend(thisMonthActive, lastMonthActive) },
                TotalDonors = new KpiItem { Amount = totalDonorsCount, VsLastMonth = CalculateTrend(thisMonthDonors, lastMonthDonors) },
                FundsDistributed = new KpiItem { Amount = fundsDistributedAmount, VsLastMonth = CalculateTrend(thisMonthDist, lastMonthDist) }
            };
        }

        public async Task<List<RecentDonationResponse>> GetLastDonationsAsync()
        {
            return await _context.Cases
                .Include(c => c.Donor)
                .Include(c => c.Category)
                .OrderByDescending(c => c.Date)
                .Take(5)
                .Select(c => new RecentDonationResponse
                {
                    Id = c.Id,
                    DonorName = c.Donor.Name,
                    Amount = c.Amount,
                    Category = c.Category.Type,
                    Date = c.Date
                })
                .ToListAsync();
        }

        public async Task<List<RecentDistributionResponse>> GetLastDistributionsAsync()
        {
            return await _context.Distributions
                .Include(d => d.Case)
                .OrderByDescending(d => d.DistributionDate)
                .Take(5)
                .Select(d => new RecentDistributionResponse
                {
                    Id = d.Id,
                    CaseName = d.Case.Description,
                    Amount = d.Amount,
                    Date = d.DistributionDate
                })
                .ToListAsync();
        }

        public async Task<DonationTrends> GetTrendsAsync()
        {
            var now = DateTime.UtcNow;

            // Monthly Trend (Current Year)
            var startOfYear = new DateTime(now.Year, 1, 1);
            var monthlyTrends = await _context.Cases
                .Where(c => c.Date >= startOfYear)
                .GroupBy(c => c.Date.Month)
                .Select(g => new MonthlyTrend { Month = g.Key, Amount = g.Sum(c => c.Amount) })
                .OrderBy(m => m.Month)
                .ToListAsync();

            // Hourly Trend (Last 24 Hours)
            var twentyFourHoursAgo = now.AddHours(-24);
            var hourlyTrends = await _context.Cases
                .Where(c => c.Date >= twentyFourHoursAgo)
                .GroupBy(c => c.Date.Hour)
                .Select(g => new HourlyTrend { Hour = g.Key, Amount = g.Sum(c => c.Amount) })
                .OrderBy(h => h.Hour)
                .ToListAsync();

            // Weekly Trend (Day of Week)
            var startOfWeek = now.AddDays(-(int)now.DayOfWeek);
            var weeklyTrends = _context.Cases
                .Where(c => c.Date >= startOfWeek)
                .AsEnumerable()
                .GroupBy(c => c.Date.DayOfWeek.ToString())
                .Select(g => new WeeklyTrend { Day = g.Key, Amount = g.Sum(c => c.Amount) })
                .ToList();

            return new DonationTrends
            {
                DonationByMonth = monthlyTrends,
                DonationByDay = hourlyTrends,
                DonationByWeek = weeklyTrends
            };
        }

        private decimal CalculateTrend(decimal current, decimal last)
        {
            if (last == 0) return current > 0 ? 100 : 0;
            return ((current - last) / last) * 100;
        }
    }
}
