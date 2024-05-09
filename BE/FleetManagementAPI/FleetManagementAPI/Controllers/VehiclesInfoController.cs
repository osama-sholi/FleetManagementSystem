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
                gvar.DicOfDic["Tags"]["STS"] = "1";
                return Ok(gvar);
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

        [HttpGet]
        public IActionResult GetVehiclesInfo()
        {
            try
            {
                GVAR gvar = _vehicleInfoService.GetAllVehiclesInfo();
                gvar.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
                gvar.DicOfDic["Tags"]["STS"] = "1";
                String json = JsonConvert.SerializeObject(gvar);
                return Ok(gvar);
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

        [HttpPost]

        public IActionResult AddVehicleInfo([FromBody] GVAR gvar)
        {
            GVAR answer = new GVAR();
            answer.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
            answer.DicOfDic["Tags"]["STS"] = "1";
            try
            {
                _vehicleInfoService.AddVehicleInfo(gvar);
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
