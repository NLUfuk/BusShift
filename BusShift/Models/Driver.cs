namespace BusShift.Models;
public class Driver : BaseEntity
{
    public required string Name { get; set; }
    public required string LicenseNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    // Navigation properties
    public ICollection<ShiftAssignment>? Shifts { get; set; } = new List<ShiftAssignment>();
}
