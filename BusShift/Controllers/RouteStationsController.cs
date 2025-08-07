using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusShift.Data;
using BusShift.Models;

namespace BusShift.Controllers
{
    public class RouteStationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RouteStationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RouteStations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RouteStations.Include(r => r.BusStation).Include(r => r.TripRoute);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RouteStations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeStation = await _context.RouteStations
                .Include(r => r.BusStation)
                .Include(r => r.TripRoute)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (routeStation == null)
            {
                return NotFound();
            }

            return View(routeStation);
        }

        // GET: RouteStations/Create
        public IActionResult Create()
        {
            ViewData["BusStationId"] = new SelectList(_context.BusStations, "Id", "Id");
            ViewData["TripRouteId"] = new SelectList(_context.TripRoutes, "Id", "Id");
            return View();
        }

        // POST: RouteStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Order,ArrivalTime,DepartureTime,TripRouteId,BusStationId,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted,DeletedAt,DeletedBy")] RouteStation routeStation)
        {
            if (ModelState.IsValid)
            {
                routeStation.Id = Guid.NewGuid();
                _context.Add(routeStation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusStationId"] = new SelectList(_context.BusStations, "Id", "Id", routeStation.BusStationId);
            ViewData["TripRouteId"] = new SelectList(_context.TripRoutes, "Id", "Id", routeStation.TripRouteId);
            return View(routeStation);
        }

        // GET: RouteStations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeStation = await _context.RouteStations.FindAsync(id);
            if (routeStation == null)
            {
                return NotFound();
            }
            ViewData["BusStationId"] = new SelectList(_context.BusStations, "Id", "Id", routeStation.BusStationId);
            ViewData["TripRouteId"] = new SelectList(_context.TripRoutes, "Id", "Id", routeStation.TripRouteId);
            return View(routeStation);
        }

        // POST: RouteStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Order,ArrivalTime,DepartureTime,TripRouteId,BusStationId,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted,DeletedAt,DeletedBy")] RouteStation routeStation)
        {
            if (id != routeStation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routeStation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteStationExists(routeStation.Id))
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
            ViewData["BusStationId"] = new SelectList(_context.BusStations, "Id", "Id", routeStation.BusStationId);
            ViewData["TripRouteId"] = new SelectList(_context.TripRoutes, "Id", "Id", routeStation.TripRouteId);
            return View(routeStation);
        }

        // GET: RouteStations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeStation = await _context.RouteStations
                .Include(r => r.BusStation)
                .Include(r => r.TripRoute)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (routeStation == null)
            {
                return NotFound();
            }

            return View(routeStation);
        }

        // POST: RouteStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var routeStation = await _context.RouteStations.FindAsync(id);
            if (routeStation != null)
            {
                _context.RouteStations.Remove(routeStation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteStationExists(Guid id)
        {
            return _context.RouteStations.Any(e => e.Id == id);
        }
    }
}
