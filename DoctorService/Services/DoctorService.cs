  using DoctorService.Data;
 using DoctorService.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Services
{
    public class DoctorServiceClass
    {
        private readonly DoctorDbContext _context;

        public DoctorServiceClass(DoctorDbContext context)
        {
            _context = context;
        }

        // Save doctor in db
        public async Task<DoctorDto> SaveDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return new DoctorDto
            {
                Name = doctor.Name,
                Email = doctor.Email,
                Phone = doctor.Phone,
                Specialty = doctor.Specialty,

            };
        }
        // Get all doctors from db
        public async Task<List<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return doctors.Select(doc => new DoctorDto
            {
                Id =doc.Id,
                Name = doc.Name,
                Email = doc.Email,
                Phone = doc.Phone,
                Specialty = doc.Specialty,
            }).ToList();
        }
        //get doc by id
        public async Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                throw new KeyNotFoundException("Doctor not found");
            }
            return new DoctorDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Email = doctor.Email,
                Phone = doctor.Phone,
                Specialty = doctor.Specialty,
            };
        }
        // Delete doctor by id
        public async Task<DoctorDto> DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                throw new KeyNotFoundException("Doctor not found");
            }
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return new DoctorDto
            {
                Name = doctor.Name,
                Email = doctor.Email,
                Phone = doctor.Phone,
                Specialty = doctor.Specialty,
            };

        }
    }
}