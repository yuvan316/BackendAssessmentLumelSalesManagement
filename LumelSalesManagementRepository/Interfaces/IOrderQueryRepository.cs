using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumelSalesManagementRepository.Models;

namespace LumelSalesManagementRepository.Interfaces
{
    public interface IOrderQueryRepository
    {
        Task<List<OrderDetails>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate);
        Task<List<(Guid ProductId, string ProductName, decimal Revenue, int QuantitySold)>> GetRevenueByProductAsync(DateTime startDate, DateTime endDate);
        Task<List<(Guid CategoryId, string CategoryName, decimal Revenue, int QuantitySold)>> GetRevenueByCategoryAsync(DateTime startDate, DateTime endDate);
        Task<List<(string Region, decimal Revenue, int QuantitySold)>> GetRevenueByRegionAsync(DateTime startDate, DateTime endDate);
    }
}
