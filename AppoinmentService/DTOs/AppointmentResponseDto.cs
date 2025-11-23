public class AppointmentResponseDto
{
   public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = "";
    public DateOnly AppointmentDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Status { get; set; }     = "Pending";
}