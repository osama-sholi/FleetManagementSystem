using FleetManagementAPI.Services;
using FPro;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : Controller
    {
        private readonly DriverService _driverService;

        public DriversController(DriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public IActionResult GetDrivers()
        {
            try
            {
                GVAR gvar = _driverService.GetAllDrivers();
                gvar.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
                gvar.DicOfDic["Tags"]["STS"] = "1";
                String json = Newtonsoft.Json.JsonConvert.SerializeObject(gvar);
                return Ok(json);
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
        public IActionResult AddDriver([FromBody]GVAR gvar)
        {
            GVAR answer = new GVAR();
            answer.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
            answer.DicOfDic["Tags"]["STS"] = "1";
            try
            {
                _driverService.AddDriver(gvar);
                return Ok(answer);
            }


            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                answer.DicOfDic["Tags"]["STS"] = "0";
                return BadRequest(answer);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDriver(long id)
        {
            GVAR answer = new GVAR();
            answer.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
            answer.DicOfDic["Tags"]["STS"] = "1";
            try
            {
                _driverService.DeleteDriver(id);
                return Ok(answer);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(answer);
            }
        }

        [HttpPut]
        public IActionResult UpdateDriver([FromBody] GVAR gvar)
        {
            GVAR answer = new GVAR();
            answer.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
            answer.DicOfDic["Tags"]["STS"] = "1";
            try
            {
                _driverService.UpdateDriver(gvar);
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
