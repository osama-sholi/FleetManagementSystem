using FleetManagementAPI.Services;
using FPro;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesInfoController : Controller
    {
        private readonly VehicleInfoService _vehicleInfoService;

        public VehiclesInfoController(VehicleInfoService vehicleInfoService)
        {
            _vehicleInfoService = vehicleInfoService;
        }

        [HttpGet("{vehicleId}")]
        public IActionResult GetVehicleInfo(long vehicleId)
        {
            try
            {
                return Ok(_vehicleInfoService.GetVehicleInfo(vehicleId));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetVehiclesInfo()
        {
            try
            {
                return Ok(_vehicleInfoService.GetAllVehiclesInfo());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddVehicleInfo([FromBody] GVAR gvar)
        {
            try
            {
                _vehicleInfoService.AddVehicleInfo(gvar);
                return Ok("Vehicle info added successfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
