
namespace AppoinmentService.Services
{

    using AppoinmentService.Data;
    using Microsoft.EntityFrameworkCore;
    using AppoinmentService.Kafka;

    public class AppoinmetServiceClass
    {
        private readonly AppoinmentDbContext _context;
        private readonly KafkaProducer _kafkaProducer;

        public AppoinmetServiceClass(AppoinmentDbContext context, KafkaProducer kafkaProducer)
        {
            _context = context;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<AppoinmentDto> AddAppoinmentAsync(Appoinment appt)
        {
            bool patientDoctorExists = await _context.Appoinments.AnyAsync(a => a.PatientId == appt.PatientId && a.DoctorId == appt.DoctorId);
            if (patientDoctorExists)
                throw new InvalidOperationException("An appointment for this patient with this doctor already exists.");

            bool doctorOvelapped = await _context.Appoinments.AnyAsync(a =>
                a.DoctorId == appt.DoctorId &&
                a.AppointmentDate == appt.AppointmentDate &&
                appt.StartTime < a.EndTime &&
                appt.EndTime > a.StartTime
            );
            if (doctorOvelapped)
                throw new InvalidOperationException("The doctor has another appointment that overlaps with the requested time.");

            _context.Appoinments.Add(appt);
            await _context.SaveChangesAsync();

            // --- Send Kafka notification ---
            await _kafkaProducer.ProduceAsync("appointments", new
            {
                appt.Id,
                appt.PatientId,
                appt.DoctorId,
                appt.AppointmentDate,
                appt.StartTime,
                appt.EndTime,
                appt.Status
            });

            return new AppoinmentDto
            {
                PatientId = appt.PatientId,
                DoctorId = appt.DoctorId,
                AppointmentDate = appt.AppointmentDate,
                StartTime = appt.StartTime,
                EndTime = appt.EndTime,
                Status = appt.Status
            };
        }

        //get all appoinments
        public async Task<List<Appoinment>> GetAllAppoinmentsAsync()
        {
            return await _context.Appoinments.ToListAsync();
        }
        //get appinment  by id
        public async Task<Appoinment> GetAppoinmentByIdAsync(int id)
        {
            var appoinment = await _context.Appoinments.FindAsync(id);
            if (appoinment == null)
            {
                throw new KeyNotFoundException("Appoinment not found");
            }
            return appoinment;
        }



        public async Task UpdateAppoinmentAsync(Appoinment appoinment)
        {
            _context.Appoinments.Update(appoinment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppoinmentAsync(int id)
        {
            var appoinment = await _context.Appoinments.FindAsync(id);
            if (appoinment != null)
            {
                _context.Appoinments.Remove(appoinment);
                await _context.SaveChangesAsync();
            }
        }
    }
}