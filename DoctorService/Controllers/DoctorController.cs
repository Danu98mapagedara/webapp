
namespace DoctorService.Controllers
{
    using DoctorService.Models;
    using DoctorService.Services;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]")]

    public class DoctorController : ControllerBase
    {
        private readonly DoctorServiceClass _doctorService;

        public DoctorController(DoctorServiceClass doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDto>> CreateDoctor([FromBody] Doctor doctor)
        {
            var savedDoctor = await _doctorService.SaveDoctorAsync(doctor);
            return Ok(savedDoctor);
        }

        [HttpGet]
        public async Task<ActionResult<List<DoctorDto>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }
    
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctorById(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
           var deletedoctr = await _doctorService.DeleteDoctorAsync(id);
            return Ok(deletedoctr);
        }
    }
}