namespace BusShift.Models;
public class ShiftAssignment : BaseEntity
{
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }

    public string? Notes { get; set; }

    public required Guid TripRouteId { get; set; }
    public required TripRoute TripRoute { get; set; } = null!;

    public required Guid VehicleId { get; set; }
    public required Vehicle Vehicle { get; set; } = null!;

    public required Guid DriverId { get; set; }
    public required Driver Driver { get; set; } = null!;
}