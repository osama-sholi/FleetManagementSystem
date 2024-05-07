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

        [HttpPost]
        public IActionResult AddVehicle([FromBody] GVAR gvar)
        {
            try
            {
                _vehicleService.AddVehicle(gvar);
                return Ok("Vehicle added successfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateVehicle([FromBody] GVAR gvar)
        {
            try
            {
                _vehicleService.UpdateVehicle(gvar);
                return Ok("Vehicle updated successfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(long id)
        {
            try
            {
                _vehicleService.DeleteVehicle(id);
                return Ok("Vehicle deleted successfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
