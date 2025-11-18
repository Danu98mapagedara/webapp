public class Appoinment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }

    // Separate date and time
    public DateOnly AppointmentDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

  public string Status { get; set; } = "Pending";
}