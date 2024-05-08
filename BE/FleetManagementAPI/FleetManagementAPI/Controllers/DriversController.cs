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

        [HttpPost]
        public IActionResult AddDriver([FromBody]GVAR gvar)
        {
            try
            {
                _driverService.AddDriver(gvar);
                return Ok("Driver added successfully");
            }


            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDriver(long id)
        {
            try
            {
                _driverService.DeleteDriver(id);
                return Ok("Driver deleted successfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateDriver([FromBody] GVAR gvar)
        {
            try
            {
                _driverService.UpdateDriver(gvar);
                return Ok("Driver updated successfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
