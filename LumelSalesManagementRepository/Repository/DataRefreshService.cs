using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LumelSalesManagementRepository.Context;
using LumelSalesManagementRepository.Interfaces;
using LumelSalesManagementRepository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LumelSalesManagementRepository.Repository
{
    public class DataRefreshService : IDataRefreshService
    {
        private readonly SalesManagementDbContext _db;
        private readonly ILogger<DataRefreshService> _log;

        public DataRefreshService(SalesManagementDbContext db, ILogger<DataRefreshService> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<DataRefreshLog> Refresh(string type)
        {
            var log = new DataRefreshLog
            {
                Type = type,
                Date = DateTime.UtcNow
            };

            try
            {
                _log.LogInformation($"Refresh started: {type}");

                if (type == "Orders" || type == "All")
                {
                    var count = await _db.OrderDetails.CountAsync();
                    log.Count += count;
                }

                if (type == "Customers" || type == "All")
                {
                    var count = await _db.CustomerDetails.CountAsync();
                    log.Count += count;
                }

                if (type == "Products" || type == "All")
                {
                    var count = await _db.ProductDetails.CountAsync();
                    log.Count += count;
                }

                log.Status = "Success";
                log.Message = "Refresh completed";
                _log.LogInformation($"Refresh completed: {type}, Records: {log.Count}");
            }
            catch (Exception ex)
            {
                log.Status = "Failed";
                log.Message = ex.Message;
                _log.LogError(ex, $"Refresh failed: {type}");
            }

            await _db.RefreshLogs.AddAsync(log);
            await _db.SaveChangesAsync();
            return log;
        }

        public async Task<List<DataRefreshLog>> GetLogs()
        {
            return await _db.RefreshLogs
                .OrderByDescending(x => x.Date)
                .Take(100)
                .ToListAsync();
        }

        public async Task<DataRefreshLog> GetLatestLog()
        {
            return await _db.RefreshLogs
                .OrderByDescending(x => x.Date)
                .FirstOrDefaultAsync();
        }
    }
}
