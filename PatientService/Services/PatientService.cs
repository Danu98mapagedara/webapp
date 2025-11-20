using Microsoft.EntityFrameworkCore;
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
            await _context.SaveChangesAsync();
            return new PatientDto
            {
                FullName = patient.FullName,
                Email = patient.Email,
                Phone = patient.Phone,
                Age = patient.Age
            };
        }
        //get all patients
        public async Task<List<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            return patients.Select(pat => new PatientDto
            {

                FullName = pat.FullName,
                Email = pat.Email,
                Phone = pat.Phone,
                Age = pat.Age
            }).ToList();
        }
        //get patient by id
        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found");
            }
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

 