public class DoctorDto
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Specialty { get; set; } = "";
    public List<AvailableSlotDto> Slots { get; set; } = new();
}