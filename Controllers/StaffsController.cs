using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportClubs1.Data;

namespace SportClubs1.Controllers
{

    public class StaffsController : Controller
    {
        private readonly SportClubsContext _context;

        public StaffsController(SportClubsContext context)
        {
            _context = context;
        }

        // GET: Staffs
        [Authorize(Roles = "Administrator,Staff")]
        public async Task<IActionResult> Index()
        {
            var sportClubsContext = _context.Staff.Include(s => s.GymAddressNavigation).Include(s => s.Schedule);
            return View(await sportClubsContext.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.GymAddressNavigation)
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            ViewData["GymAddress"] = new SelectList(_context.Gyms, "Address", "Address");
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id");
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,ScheduleId,GymAddress,Name,Sername,Profession,Pfp")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GymAddress"] = new SelectList(_context.Gyms, "Address", "Address", staff.GymAddress);
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id", staff.ScheduleId);
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            ViewData["GymAddress"] = new SelectList(_context.Gyms, "Address", "Address", staff.GymAddress);
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id", staff.ScheduleId);
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,ScheduleId,GymAddress,Name,Sername,Profession,Pfp")] Staff staff)
        {
            if (id != staff.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.StaffId))
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
            ViewData["GymAddress"] = new SelectList(_context.Gyms, "Address", "Address", staff.GymAddress);
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id", staff.ScheduleId);
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.GymAddressNavigation)
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.StaffId == id);
        }
    }
}
