using BusShift.Models;

namespace BusShift.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
    public DbSet<BusStation> BusStations { get; set; } = null!;
    public DbSet<Driver> Drivers { get; set; } = null!;
    public DbSet<Passenger> Passengers { get; set; } = null!;
    public DbSet<RouteStation> RouteStations { get; set; } = null!;
    public DbSet<Seat> Seats { get; set; } = null!;
    public DbSet<TripRoute> TripRoutes { get; set; } = null!;
    public DbSet<ShiftAssignment> ShiftAssignments { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
}


