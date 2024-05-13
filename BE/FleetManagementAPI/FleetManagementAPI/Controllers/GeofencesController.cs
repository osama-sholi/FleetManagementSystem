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

        [HttpGet("circular")]
        public IActionResult GetCircularGeofences()
        {
            try
            {
                GVAR gvar = _geofenceService.GetCircularGeofences();
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

        [HttpGet("rectangular")]
        public IActionResult GetRectangularGeofences()
        {
            try
            {
                GVAR gvar = _geofenceService.GetRectangularGeofences();
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

        [HttpGet("polygon")]
        public IActionResult GetPolygonGeofences()
        {
            try
            {
                GVAR gvar = _geofenceService.GetPolygonGeofences();
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
    }
}