using FleetManagementAPI.Services;
using FPro;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesHistoryController : Controller
    {
        private readonly RouteHistoryService _routesHistoryService;

        public RoutesHistoryController(RouteHistoryService routesHistoryService)
        {
            _routesHistoryService = routesHistoryService;
        }

        [HttpGet("{vehicleId}, {startDate}, {endDate}")]
        public IActionResult GetRoutesHistory(long vehicleId, long startDate, long endDate)
        {
            try
            {
                return Ok(_routesHistoryService.GetVehicleRouteHistory(vehicleId, startDate, endDate));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddRouteHistory([FromBody] GVAR gvar)
        {
            try
            {
                _routesHistoryService.AddRouteHistory(gvar);
                return Ok("Route history added successfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
