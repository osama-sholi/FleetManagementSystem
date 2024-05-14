using FleetManagementAPI.Services;
using FPro;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        // GET: api/RoutesHistory/{vehicleId}/{startDate}/{endDate}
        [HttpGet("{vehicleId}/{startDate}/{endDate}")]
        public IActionResult GetRoutesHistory(long vehicleId, long startDate, long endDate)
        {
            try
            {
                GVAR gvar = _routesHistoryService.GetVehicleRouteHistory(vehicleId, startDate, endDate);
                gvar.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
                gvar.DicOfDic["Tags"]["STS"] = "1";
                String json = JsonConvert.SerializeObject(gvar);
                return Ok(json);
            }
            catch (ResourseNotFoundException ex)
            {
                Console.WriteLine(ex);
                GVAR gvar = new GVAR();
                gvar.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
                gvar.DicOfDic["Tags"]["STS"] = "0";
                return NotFound(gvar);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                GVAR gvar = new GVAR();
                gvar.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
                gvar.DicOfDic["Tags"]["STS"] = "0";
                return BadRequest(gvar);
            }
        }

        // POST: api/RoutesHistory
        [HttpPost]
        public IActionResult AddRouteHistory([FromBody] GVAR gvar)
        {
            GVAR answer = new GVAR();
            answer.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
            answer.DicOfDic["Tags"]["STS"] = "1";
            try
            {
                _routesHistoryService.AddRouteHistory(gvar);
                return Ok(answer);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                answer.DicOfDic["Tags"]["STS"] = "0";
                return BadRequest(answer);
            }
        }
    }
}
