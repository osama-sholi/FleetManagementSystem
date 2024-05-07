using FleetManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

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
                return Ok(_geofenceService.GetGeofences());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("circular")]
        public IActionResult GetCircularGeofences()
        {
            try
            {
                return Ok(_geofenceService.GetCircularGeofences());
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
                return Ok(_geofenceService.GetRectangularGeofences());
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
                return Ok(_geofenceService.GetPolygonGeofences());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}