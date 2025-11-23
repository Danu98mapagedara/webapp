using AppoinmentService.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AppoinmentService.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AppoinmentController : ControllerBase
    {
        private readonly AppoinmetServiceClass _appoinmetService;

        public AppoinmentController(AppoinmetServiceClass appoinmetService)
        {
            _appoinmetService = appoinmetService;
        }
 [HttpPost]
        public async Task<ActionResult<AppoinmentDto>> AddAppoinment([FromBody] Appoinment appoinment)
        {
           var Getappoinmet = await _appoinmetService.AddAppoinmentAsync(appoinment);
          return  Ok(Getappoinmet);
        }
        [HttpGet]
        public async Task<ActionResult<List<Appoinment>>> GetAllAppoinments()
        {
            var appoinments = await _appoinmetService.GetAllAppoinmentsAsync();
            return Ok(appoinments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appoinment>> GetAppoinmentById(int id)
        {
            var appoinment = await _appoinmetService. GetAppointmentByIdAsync(id);
            if (appoinment == null)
            {
                return NotFound();
            }
            return Ok(appoinment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAppoinment(int id)
        {
            await _appoinmetService.DeleteAppoinmentAsync(id);
            return NoContent();
        }
    }

}