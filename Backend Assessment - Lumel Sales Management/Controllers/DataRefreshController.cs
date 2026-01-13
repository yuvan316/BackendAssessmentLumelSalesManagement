using LumelSalesManagementRepository.Interfaces;
using LumelSalesManagementRepository.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lumel_Sales_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataRefreshController : ControllerBase
    {
        private readonly ILogger<DataRefreshController> _log;
        private readonly IDataRefreshService _service;

        public DataRefreshController(ILogger<DataRefreshController> log, IDataRefreshService service)
        {
            _log = log;
            _service = service;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromQuery] string type = "All")
        {
            try
            {
                _log.LogInformation($"Refresh request: {type}");
                var result = await _service.Refresh(type);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Refresh error");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("logs")]
        public async Task<IActionResult> GetLogs()
        {
            try
            {
                var logs = await _service.GetLogs();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Get logs error");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("logs/latest")]
        public async Task<IActionResult> GetLatestLog()
        {
            try
            {
                var log = await _service.GetLatestLog();
                if (log == null)
                    return NotFound(new { message = "No logs found" });
                
                return Ok(log);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Get latest log error");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
