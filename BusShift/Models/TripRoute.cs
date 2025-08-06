namespace BusShift.Models;

public class TripRoute : BaseEntity
{
    public required string Name { get; set; }
    public required TimeSpan Duration { get; set; }
    public string? Notes { get; set; }

    //RouteStations
    public ICollection<RouteStation> RouteStations { get; set; } = new List<RouteStation>();

}
