using FleetManagementAPI.Services;
using FPro;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using Newtonsoft.Json;

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
                GVAR gvar = _vehicleInfoService.GetVehicleInfo(vehicleId);
                String json = JsonConvert.SerializeObject(gvar);
                return Ok(json);
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
                GVAR gvar = _vehicleInfoService.GetAllVehiclesInfo();
                String json = JsonConvert.SerializeObject(gvar);
                return Ok(json);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
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
