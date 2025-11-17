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
                Slots = doctor.Slots.Select(slot => new AvailableSlotDto
                {
                    StartTime = slot.StartTime,
                    EndTime = slot.EndTime
                }).ToList()
            };
        }
    }
}