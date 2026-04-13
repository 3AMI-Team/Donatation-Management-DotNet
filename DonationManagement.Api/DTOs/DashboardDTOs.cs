using System;
using System.Collections.Generic;

namespace DonationManagement.Api.DTOs
{
    public class DashboardResponse
    {
        public DashboardKpis Kpis { get; set; } = new();
        public List<RecentDonationResponse> LastDonations { get; set; } = new();
        public List<RecentDistributionResponse> LastDistributions { get; set; } = new();
        public DonationTrends Trends { get; set; } = new();
    }

    public class DashboardKpis
    {
        public KpiItem TotalDonations { get; set; } = new();
        public KpiItem ActiveCases { get; set; } = new();
        public KpiItem TotalDonors { get; set; } = new();
        public KpiItem FundsDistributed { get; set; } = new();
    }

    public class KpiItem
    {
        public decimal Amount { get; set; }
        public decimal VsLastMonth { get; set; }
    }

    public class RecentDonationResponse
    {
        public int Id { get; set; }
        public string DonorName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Category { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }

    public class RecentDistributionResponse
    {
        public int Id { get; set; }
        public string CaseName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class DonationTrends
    {
        public List<MonthlyTrend> DonationByMonth { get; set; } = new();
        public List<HourlyTrend> DonationByDay { get; set; } = new();
        public List<WeeklyTrend> DonationByWeek { get; set; } = new();
    }

    public class MonthlyTrend
    {
        public int Month { get; set; }
        public decimal Amount { get; set; }
    }

    public class HourlyTrend
    {
        public int Hour { get; set; }
        public decimal Amount { get; set; }
    }

    public class WeeklyTrend
    {
        public string Day { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
