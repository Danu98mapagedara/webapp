namespace PatientService.DTOs
{
    
    public class PatientDto
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public int Age { get; set; }
    }
}