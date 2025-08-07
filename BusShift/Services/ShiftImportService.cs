using BusShift.Data;
using BusShift.Models;

namespace BusShift.Services;

public class ShiftImportService
{
    private readonly ApplicationDbContext _context;

    public ShiftImportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void ImportMockData()
    {
        var exampleDriver = _context.Drivers.FirstOrDefault();
        var exampleRoute = _context.TripRoutes.FirstOrDefault();
        var exampleVehicle = _context.Vehicles.FirstOrDefault();

        if (exampleDriver == null || exampleRoute == null || exampleVehicle == null)
            return;

        for (int i = 1; i <= 10; i++) // 1-10 Temmuz
        {
            var shift = new ShiftAssignment
            {
                StartTime = new DateTime(2025, 7, i, 6, 25, 0),
                EndTime = new DateTime(2025, 7, i, 7, 25, 0),
                DriverId = exampleDriver.Id,
                TripRouteId = exampleRoute.Id,
                VehicleId = exampleVehicle.Id,
                Notes = "Demo Import",
                // Required members
                CreatedBy = "import", // Set as appropriate for your context
                TripRoute = exampleRoute,
                Vehicle = exampleVehicle,
                Driver = exampleDriver
            };

            _context.ShiftAssignments.Add(shift);
        }

        _context.SaveChanges();
    }
}
