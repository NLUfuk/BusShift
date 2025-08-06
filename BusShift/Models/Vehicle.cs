namespace BusShift.Models;
public class Vehicle : BaseEntity
{
    public required string LicensePlate { get; set; }
    public required string Model { get; set; }
    public int Capacity { get; set; }
    public string? Notes { get; set; }

    // Navigation properties
    public ICollection<ShiftAssignment>? ShiftAssignments { get; set; } = new List<ShiftAssignment>();

    //seats collection
    public ICollection<Seat>? Seats { get; set; } = new List<Seat>();
}
