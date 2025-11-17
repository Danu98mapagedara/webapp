using PatientService.Data;
using PatientService.DTOs;
using PatientService.Models;

namespace PatientService.Services
{
    public class PatientServiceClass
    {
        private readonly PatientDbContext _context;
        public PatientServiceClass(PatientDbContext context)
        {
            _context = context;
        }
        //save  patient  in db
        public async Task<PatientDto> SavePatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await  _context.SaveChangesAsync();
            return new PatientDto
            {
                FullName = patient.FullName,
                Email = patient.Email,
                Phone = patient.Phone,
                Age = patient.Age
            };
        }

    } 
}

 