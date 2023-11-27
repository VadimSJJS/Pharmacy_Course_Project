using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmacy_Project.Models;
using Pharmacy_Project.ViewModel;
using Pharmacy_Project.ViewModels;

namespace Pharmacy_Project.Controllers
{
    [Authorize]
    public class IncomingController : Controller
    {
        private readonly PharmacyContext _context;
        private readonly int pageSize = 10;

        public IncomingController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Incoming
        public async Task<IActionResult> Index(int page = 1)
        {
            IncomingsViewModel incomingsModel;
            IQueryable<Incoming> pharmacyContext = _context.Incomings.Include(i => i.Medicine);
            var count = pharmacyContext.Count();
            pharmacyContext = pharmacyContext.Skip((page - 1) * pageSize).Take(pageSize);

            incomingsModel = new IncomingsViewModel
            {
                Incomings = pharmacyContext,
                Page = new PageViewModel(count, page, pageSize)

            };

            return View(incomingsModel);
        }

        // GET: Incoming/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Incomings == null)
            {
                return NotFound();
            }

            var incoming = await _context.Incomings
                .Include(i => i.Medicine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incoming == null)
            {
                return NotFound();
            }

            return View(incoming);
        }

        // GET: Incoming/Create
        public IActionResult Create()
        {
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name");
            return View();
        }

        // POST: Incoming/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MedicineId,ArrivalDate,Count,Provider,Price")] Incoming incoming)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incoming);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", incoming.MedicineId);
            return View(incoming);
        }

        // GET: Incoming/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Incomings == null)
            {
                return NotFound();
            }

            var incoming = await _context.Incomings.FindAsync(id);
            if (incoming == null)
            {
                return NotFound();
            }
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", incoming.MedicineId);
            return View(incoming);
        }

        // POST: Incoming/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedicineId,ArrivalDate,Count,Provider,Price")] Incoming incoming)
        {
            if (id != incoming.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incoming);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomingExists(incoming.Id))
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
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Id", incoming.MedicineId);
            return View(incoming);
        }

        // GET: Incoming/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Incomings == null)
            {
                return NotFound();
            }

            var incoming = await _context.Incomings
                .Include(i => i.Medicine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incoming == null)
            {
                return NotFound();
            }

            return View(incoming);
        }

        // POST: Incoming/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Incomings == null)
            {
                return Problem("Entity set 'PharmacyContext.Incomings'  is null.");
            }
            var incoming = await _context.Incomings.FindAsync(id);
            if (incoming != null)
            {
                _context.Incomings.Remove(incoming);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomingExists(int id)
        {
          return (_context.Incomings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
