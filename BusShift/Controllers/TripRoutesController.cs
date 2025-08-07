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
    public class TripRoutesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripRoutesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TripRoutes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TripRoutes.ToListAsync());
        }

        // GET: TripRoutes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripRoute = await _context.TripRoutes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripRoute == null)
            {
                return NotFound();
            }

            return View(tripRoute);
        }

        // GET: TripRoutes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TripRoutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Duration,Notes,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted,DeletedAt,DeletedBy")] TripRoute tripRoute)
        {
            if (ModelState.IsValid)
            {
                tripRoute.Id = Guid.NewGuid();
                _context.Add(tripRoute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripRoute);
        }

        // GET: TripRoutes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripRoute = await _context.TripRoutes.FindAsync(id);
            if (tripRoute == null)
            {
                return NotFound();
            }
            return View(tripRoute);
        }

        // POST: TripRoutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Duration,Notes,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted,DeletedAt,DeletedBy")] TripRoute tripRoute)
        {
            if (id != tripRoute.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripRoute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripRouteExists(tripRoute.Id))
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
            return View(tripRoute);
        }

        // GET: TripRoutes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripRoute = await _context.TripRoutes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripRoute == null)
            {
                return NotFound();
            }

            return View(tripRoute);
        }

        // POST: TripRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tripRoute = await _context.TripRoutes.FindAsync(id);
            if (tripRoute != null)
            {
                _context.TripRoutes.Remove(tripRoute);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripRouteExists(Guid id)
        {
            return _context.TripRoutes.Any(e => e.Id == id);
        }
    }
}
