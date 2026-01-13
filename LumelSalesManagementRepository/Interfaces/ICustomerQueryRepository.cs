using LumelSalesManagementRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumelSalesManagementRepository.Interfaces
{
    public interface ICustomerQueryRepository
    {
        Task<CustomerDetails> GetCustomerByIdAsync(Guid customerId);
        Task<List<CustomerDetails>> GetAllCustomersAsync();
    }
}
