using System;
using System.Collections.Generic;

namespace LumelSalesManagementDomain.Models
{
    public class RevenueRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RevenueType RevenueType { get; set; }
    }

    public class TotalRevenueResponse
    {
        public decimal TotalRevenue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class RevenueByProductResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public int QuantitySold { get; set; }
    }

    public class RevenueByProductListResponse
    {
        public List<RevenueByProductResponse> Products { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class RevenueByCategoryResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public int QuantitySold { get; set; }
    }

    public class RevenueByCategoryListResponse
    {
        public List<RevenueByCategoryResponse> Categories { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class RevenueByRegionResponse
    {
        public string Region { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public int QuantitySold { get; set; }
    }

    public class RevenueByRegionListResponse
    {
        public List<RevenueByRegionResponse> Regions { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

