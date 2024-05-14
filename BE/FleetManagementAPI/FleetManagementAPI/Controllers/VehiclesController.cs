using FleetManagementAPI.Services;
using FPro;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : Controller
    {
        private readonly VehicleService _vehicleService;

        public VehiclesController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // GET: api/Vehicles
        [HttpPost]
        public IActionResult AddVehicle([FromBody] GVAR gvar)
        {
            GVAR answer = new GVAR();
            answer.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
            answer.DicOfDic["Tags"]["STS"] = "1";
            try
            {
                _vehicleService.AddVehicle(gvar);
                return Ok(answer);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                answer.DicOfDic["Tags"]["STS"] = "0";
                return BadRequest(answer);
            }
        }

        // GET: api/Vehicles
        [HttpPut]
        public IActionResult UpdateVehicle([FromBody] GVAR gvar)
        {
            GVAR answer = new GVAR();
            answer.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
            answer.DicOfDic["Tags"]["STS"] = "1";
            try
            {
                _vehicleService.UpdateVehicle(gvar);
                return Ok(answer);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                answer.DicOfDic["Tags"]["STS"] = "0";
                return BadRequest(answer);
            }
        }

        // GET: api/Vehicles
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(long id)
        {
            GVAR answer = new GVAR();
            answer.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
            answer.DicOfDic["Tags"]["STS"] = "1";
            try
            {
                _vehicleService.DeleteVehicle(id);
                return Ok(answer);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(answer);
            }
        }
    }
}
