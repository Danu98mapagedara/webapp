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
        [HttpGet]
        public async Task<ActionResult<List<PatientDto>>> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatientById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
    }
}