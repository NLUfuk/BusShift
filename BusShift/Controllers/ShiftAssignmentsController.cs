using BusShift.Data;
using BusShift.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusShift.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShiftAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShiftAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShiftAssignments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShiftAssignments.Include(s => s.Driver).Include(s => s.TripRoute).Include(s => s.Vehicle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ShiftAssignments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftAssignment = await _context.ShiftAssignments
                .Include(s => s.Driver)
                .Include(s => s.TripRoute)
                .Include(s => s.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shiftAssignment == null)
            {
                return NotFound();
            }

            return View(shiftAssignment);
        }

        // GET: ShiftAssignments/Create
        public IActionResult Create()
        {
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id");
            ViewData["TripRouteId"] = new SelectList(_context.TripRoutes, "Id", "Id");
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id");
            return View();
        }

        // POST: ShiftAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartTime,EndTime,Notes,TripRouteId,VehicleId,DriverId,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted,DeletedAt,DeletedBy")] ShiftAssignment shiftAssignment)
        {
            if (ModelState.IsValid)
            {
                shiftAssignment.Id = Guid.NewGuid();
                _context.Add(shiftAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", shiftAssignment.DriverId);
            ViewData["TripRouteId"] = new SelectList(_context.TripRoutes, "Id", "Id", shiftAssignment.TripRouteId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", shiftAssignment.VehicleId);
            return View(shiftAssignment);
        }

        // GET: ShiftAssignments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftAssignment = await _context.ShiftAssignments.FindAsync(id);
            if (shiftAssignment == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", shiftAssignment.DriverId);
            ViewData["TripRouteId"] = new SelectList(_context.TripRoutes, "Id", "Id", shiftAssignment.TripRouteId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", shiftAssignment.VehicleId);
            return View(shiftAssignment);
        }

        // POST: ShiftAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StartTime,EndTime,Notes,TripRouteId,VehicleId,DriverId,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted,DeletedAt,DeletedBy")] ShiftAssignment shiftAssignment)
        {
            if (id != shiftAssignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shiftAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftAssignmentExists(shiftAssignment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", shiftAssignment.DriverId);
            ViewData["TripRouteId"] = new SelectList(_context.TripRoutes, "Id", "Id", shiftAssignment.TripRouteId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", shiftAssignment.VehicleId);
            return View(shiftAssignment);
        }

        // GET: ShiftAssignments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftAssignment = await _context.ShiftAssignments
                .Include(s => s.Driver)
                .Include(s => s.TripRoute)
                .Include(s => s.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shiftAssignment == null)
            {
                return NotFound();
            }

            return View(shiftAssignment);
        }

        // POST: ShiftAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shiftAssignment = await _context.ShiftAssignments.FindAsync(id);
            if (shiftAssignment != null)
            {
                _context.ShiftAssignments.Remove(shiftAssignment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftAssignmentExists(Guid id)
        {
            return _context.ShiftAssignments.Any(e => e.Id == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportMockShifts()
        {
            var exampleDriver = _context.Drivers.FirstOrDefault();
            var exampleRoute = _context.TripRoutes.FirstOrDefault();
            var exampleVehicle = _context.Vehicles.FirstOrDefault();

            if (exampleDriver == null || exampleRoute == null || exampleVehicle == null)
            {
                TempData["Error"] = "Driver, Route veya Vehicle eksik!";
                return RedirectToAction(nameof(Index));
            }

            for (int day = 1; day <= 10; day++) // İlk 10 gün için
            {
                var shift = new ShiftAssignment
                {
                    Id = Guid.NewGuid(),
                    StartTime = new DateTime(2025, 7, day, 6, 25, 0),
                    EndTime = new DateTime(2025, 7, day, 7, 25, 0),
                    DriverId = exampleDriver.Id,
                    TripRouteId = exampleRoute.Id,
                    VehicleId = exampleVehicle.Id,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "import",
                    TripRoute = exampleRoute,
                    Vehicle = exampleVehicle,
                    Driver = exampleDriver,
                    Notes = "Mock - Excel'den alınacak"
                };

                _context.ShiftAssignments.Add(shift);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "10 günlük shift başarıyla import edildi.";
            return RedirectToAction(nameof(Index));
        }

    }
}
