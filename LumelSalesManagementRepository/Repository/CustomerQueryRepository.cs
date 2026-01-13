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
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private readonly SalesManagementDbContext _dbContext;

        public CustomerQueryRepository(SalesManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerDetails> GetCustomerByIdAsync(Guid customerId)
        {
            return await _dbContext.CustomerDetails.FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task<List<CustomerDetails>> GetAllCustomersAsync()
        {
            return await _dbContext.CustomerDetails.ToListAsync();
        }
    }
}
