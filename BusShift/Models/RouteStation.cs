namespace BusShift.Models;

public class RouteStation : BaseEntity
{
    public int Order { get; set; } // Order of the station in the route
    public TimeSpan ArrivalTime { get; set; } // Expected arrival time at the station
    public TimeSpan DepartureTime { get; set; } // Expected departure time from the station
    
    // Navigation properties
    public required TripRoute TripRoute { get; set; } = null!;
    public required Guid TripRouteId { get; set; }

    public required BusStation BusStation { get; set; } = null!;
    public required Guid BusStationId { get; set; }

}