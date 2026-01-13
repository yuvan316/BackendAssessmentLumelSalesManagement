using System;

namespace LumelSalesManagementRepository.Models
{
    public class DataRefreshLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = string.Empty;
        public int Count { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
