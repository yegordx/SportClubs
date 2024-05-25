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

    public class TrainingMachinesController : Controller
    {
        private readonly SportClubsContext _context;

        public TrainingMachinesController(SportClubsContext context)
        {
            _context = context;
        }

        // GET: TrainingMachines
        [Authorize(Roles = "Administrator,Staff")]
        public async Task<IActionResult> Index()
        {
            var sportClubsContext = _context.TrainingMachines.Include(t => t.GymAddressNavigation);
            return View(await sportClubsContext.ToListAsync());
        }

        // GET: TrainingMachines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingMachine = await _context.TrainingMachines
                .Include(t => t.GymAddressNavigation)
                .FirstOrDefaultAsync(m => m.InventNum == id);
            if (trainingMachine == null)
            {
                return NotFound();
            }

            return View(trainingMachine);
        }

        // GET: TrainingMachines/Create
        public IActionResult Create()
        {
            ViewData["GymAddress"] = new SelectList(_context.Gyms, "Address", "Address");
            return View();
        }

        // POST: TrainingMachines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventNum,GymAddress,Cost,State,Name")] TrainingMachine trainingMachine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingMachine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GymAddress"] = new SelectList(_context.Gyms, "Address", "Address", trainingMachine.GymAddress);
            return View(trainingMachine);
        }

        // GET: TrainingMachines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingMachine = await _context.TrainingMachines.FindAsync(id);
            if (trainingMachine == null)
            {
                return NotFound();
            }
            ViewData["GymAddress"] = new SelectList(_context.Gyms, "Address", "Address", trainingMachine.GymAddress);
            return View(trainingMachine);
        }

        // POST: TrainingMachines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventNum,GymAddress,Cost,State,Name")] TrainingMachine trainingMachine)
        {
            if (id != trainingMachine.InventNum)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingMachine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingMachineExists(trainingMachine.InventNum))
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
            ViewData["GymAddress"] = new SelectList(_context.Gyms, "Address", "Address", trainingMachine.GymAddress);
            return View(trainingMachine);
        }

        // GET: TrainingMachines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingMachine = await _context.TrainingMachines
                .Include(t => t.GymAddressNavigation)
                .FirstOrDefaultAsync(m => m.InventNum == id);
            if (trainingMachine == null)
            {
                return NotFound();
            }

            return View(trainingMachine);
        }

        // POST: TrainingMachines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingMachine = await _context.TrainingMachines.FindAsync(id);
            if (trainingMachine != null)
            {
                _context.TrainingMachines.Remove(trainingMachine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingMachineExists(int id)
        {
            return _context.TrainingMachines.Any(e => e.InventNum == id);
        }
    }
}
