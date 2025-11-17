namespace PatientService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PatientService.DTOs;
    using PatientService.Models;
    using PatientService.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientServiceClass _patientService;

        public PatientController(PatientServiceClass patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<ActionResult<PatientDto>> CreatePatient([FromBody] Patient patient)
        {
            var savedPatient = await _patientService.SavePatientAsync(patient);
            return Ok(savedPatient);
        }
    }
}