using FleetManagementAPI.Services;
using FPro;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FleetManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeofencesController : Controller
    {
        private readonly GeofenceService _geofenceService;

        public GeofencesController(GeofenceService geofenceService)
        {
            _geofenceService = geofenceService;
        }

        [HttpGet]
        public IActionResult GetGeofences()
        {
            try
            {
                GVAR gvar = _geofenceService.GetGeofences();
                String json = JsonConvert.SerializeObject(gvar);
                return Ok(json);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("circular")]
        public IActionResult GetCircularGeofences()
        {
            try
            {
                GVAR gvar = _geofenceService.GetCircularGeofences();
                String json = JsonConvert.SerializeObject(gvar);
                return Ok(json);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("rectangular")]
        public IActionResult GetRectangularGeofences()
        {
            try
            {
                GVAR gvar = _geofenceService.GetRectangularGeofences();
                String json = JsonConvert.SerializeObject(gvar);
                return Ok(json);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("polygon")]
        public IActionResult GetPolygonGeofences()
        {
            try
            {
                GVAR gvar = _geofenceService.GetPolygonGeofences();
                String json = JsonConvert.SerializeObject(gvar);
                return Ok(json);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}