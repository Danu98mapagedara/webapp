namespace DoctorService.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Specialty { get; set; } = "";

        public List<AvailableSlot> Slots { get; set; } = new();
    }
    public class AvailableSlot
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
    }
}