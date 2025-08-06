namespace BusShift.Models;

public class Seat : BaseEntity
{
    public required string SeatNumber { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string? Notes { get; set; }
    // Navigation properties
    public required Vehicle Vehicle { get; set; } = null!;
    public Guid? VehicleId { get; set; }
  
}
