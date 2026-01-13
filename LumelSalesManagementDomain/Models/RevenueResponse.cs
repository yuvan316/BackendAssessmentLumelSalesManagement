using System;
using System.Collections.Generic;

namespace LumelSalesManagementDomain.Models
{
    public class RevenueResponse
    {
        public RevenueType RevenueType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? TotalRevenue { get; set; }
        public List<RevenueByProductResponse> Products { get; set; } = new();
        public List<RevenueByCategoryResponse> Categories { get; set; } = new();
        public List<RevenueByRegionResponse> Regions { get; set; } = new();
    }
}
