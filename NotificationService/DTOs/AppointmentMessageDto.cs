namespace NotificationService.DTOs
{
    public class AppointmentMessageDto
    {
         public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateOnly AppointmentDate { get; set; }  // Correct
        public TimeOnly StartTime { get; set; }        // Correct
        public TimeOnly EndTime { get; set; }          // Correct
        public string Status { get; set; }="Pending";
    }
}