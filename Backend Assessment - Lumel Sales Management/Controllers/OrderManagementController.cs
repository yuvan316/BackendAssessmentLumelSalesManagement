using LumelSalesManagementDomain.Domain.Interfaces;
using LumelSalesManagementDomain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lumel_Sales_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagementController : ControllerBase
    {
        private readonly ILogger<OrderManagementController> _logger;
        private readonly IOrderManagementProcessor _orderProcessor;
        
        public OrderManagementController(ILogger<OrderManagementController> logger, IOrderManagementProcessor orderProcessor)
        {
            _logger = logger;
            _orderProcessor = orderProcessor;
        }

        [HttpPost("revenue")]
        public async Task<ActionResult<RevenueResponse>> GetRevenue([FromBody] RevenueRequest request)
        {
            try
            {
                if (request.StartDate > request.EndDate)
                {
                    return BadRequest("StartDate must be less than or equal to EndDate");
                }

                var result = await _orderProcessor.GetRevenueAsync(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid argument for revenue ");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving revenue ");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
