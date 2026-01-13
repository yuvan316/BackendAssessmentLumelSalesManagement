using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LumelSalesManagementRepository.Models;

namespace LumelSalesManagementRepository.Interfaces
{
    public interface IDataRefreshService
    {
        Task<DataRefreshLog> Refresh(string type);
        Task<List<DataRefreshLog>> GetLogs();
        Task<DataRefreshLog> GetLatestLog();
    }
}
