namespace BusShift.Models;

public class Passenger : BaseEntity
{
    public required string Name { get; set; }
    public GenderEnum Gender { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public string? Notes { get; set; }
    // Navigation properties
    public ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
}
