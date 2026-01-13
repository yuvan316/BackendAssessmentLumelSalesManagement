using LumelSalesManagementRepository.Context;
using LumelSalesManagementRepository.Interfaces;
using LumelSalesManagementRepository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumelSalesManagementRepository.Repository
{
    public class OrderQueryRepository : IOrderQueryRepository
    {
        private readonly SalesManagementDbContext _dbContext;

        public OrderQueryRepository(SalesManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderDetails>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbContext.OrderDetails
                .Where(o => o.DateOfSale >= startDate && o.DateOfSale <= endDate)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate)
        {
            var totalRevenue = await _dbContext.OrderDetails
                .Where(o => o.DateOfSale >= startDate && o.DateOfSale <= endDate)
                .SumAsync(o => (decimal)o.QuantitySold);

            return totalRevenue;
        }

        public async Task<List<(Guid ProductId, string ProductName, decimal Revenue, int QuantitySold)>> GetRevenueByProductAsync(DateTime startDate, DateTime endDate)
        {
            var revenueByProduct = await _dbContext.OrderDetails
                .Where(o => o.DateOfSale >= startDate && o.DateOfSale <= endDate)
                .GroupBy(o => new { o.ProductId })
                .Select(g => new
                {
                    g.Key.ProductId,
                    Revenue = (decimal)g.Sum(o => o.QuantitySold),
                    QuantitySold = g.Sum(o => o.QuantitySold)
                })
                .ToListAsync();

            // Map to tuple format with placeholder product names (you may need to join with ProductDetails if needed)
            return revenueByProduct.Select(r => (r.ProductId, "Product", r.Revenue, r.QuantitySold)).ToList();
        }

        public async Task<List<(Guid CategoryId, string CategoryName, decimal Revenue, int QuantitySold)>> GetRevenueByCategoryAsync(DateTime startDate, DateTime endDate)
        {
            var revenueByCategory = await _dbContext.OrderDetails
                .Where(o => o.DateOfSale >= startDate && o.DateOfSale <= endDate)
                .GroupBy(o => new { o.CategoryId })
                .Select(g => new
                {
                    g.Key.CategoryId,
                    Revenue = (decimal)g.Sum(o => o.QuantitySold),
                    QuantitySold = g.Sum(o => o.QuantitySold)
                })
                .ToListAsync();

            return revenueByCategory.Select(r => (r.CategoryId, "Category", r.Revenue, r.QuantitySold)).ToList();
        }

        public async Task<List<(string Region, decimal Revenue, int QuantitySold)>> GetRevenueByRegionAsync(DateTime startDate, DateTime endDate)
        {
            var revenueByRegion = await _dbContext.OrderDetails
                .Where(o => o.DateOfSale >= startDate && o.DateOfSale <= endDate)
                .GroupBy(o => new { o.Region })
                .Select(g => new
                {
                    g.Key.Region,
                    Revenue = (decimal)g.Sum(o => o.QuantitySold),
                    QuantitySold = g.Sum(o => o.QuantitySold)
                })
                .ToListAsync();

            return revenueByRegion.Select(r => (r.Region, r.Revenue, r.QuantitySold)).ToList();
        }
    }
}
