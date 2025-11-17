
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
    }
}