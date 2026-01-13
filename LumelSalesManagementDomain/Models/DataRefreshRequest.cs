using System;

namespace LumelSalesManagementDomain.Models
{
    public class DataRefreshRequest
    {
        public string RefreshType { get; set; } = "All";
        public bool OverwriteExisting { get; set; } = false;
    }

    public class DataRefreshResponse
    {
        public Guid RefreshLogId { get; set; }
        public string RefreshType { get; set; } = string.Empty;
        public DateTime RefreshStartTime { get; set; }
        public DateTime? RefreshEndTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public int RecordsProcessed { get; set; }
        public int RecordsAdded { get; set; }
        public int RecordsUpdated { get; set; }
        public int DuplicatesFound { get; set; }
        public string? ErrorMessage { get; set; }
        public TimeSpan? Duration { get; set; }
    }

    public class RefreshLogsResponse
    {
        public List<DataRefreshResponse> Logs { get; set; } = new();
        public int TotalLogs { get; set; }
    }
}
