using LumelSalesManagementDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumelSalesManagementDomain.Domain.Interfaces
{
    public interface IOrderManagementProcessor
    {
        Task<RevenueResponse> GetRevenueAsync(RevenueRequest request);
    }
}
