namespace AppoinmentService.Services
{
  
    using AppoinmentService.Data;
    using Microsoft.EntityFrameworkCore;

    public class AppoinmetServiceClass
    {
        private readonly AppoinmentDbContext _context;

        public AppoinmetServiceClass(AppoinmentDbContext context)
        {
            _context = context;
        }
 public async Task<AppoinmentDto> AddAppoinmentAsync(Appoinment appt)
{
            bool patientDoctorExists = await _context.Appoinments.AnyAsync(a => a.PatientId == appt.PatientId && a.DoctorId == appt.DoctorId);
            if (patientDoctorExists)
            {
                throw new InvalidOperationException("An appointment for this patient with this doctor already exists.");
    }
    bool doctorOvelapped=await  _context.Appoinments.AnyAsync(a =>
            a.DoctorId == appt.DoctorId &&
            a.AppointmentDate == appt.AppointmentDate &&
            appt.StartTime < a.EndTime &&
            appt.EndTime > a.StartTime
        );
    if (doctorOvelapped)
    {
        throw new InvalidOperationException("The doctor has another appointment that overlaps with the requested time.");
    }
    _context. Appoinments.Add(appt);
    await _context.SaveChangesAsync();

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


        public async Task<List<Appoinment>> GetAllAppoinmentsAsync()
        {
            return await _context.Appoinments.ToListAsync();
        }

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