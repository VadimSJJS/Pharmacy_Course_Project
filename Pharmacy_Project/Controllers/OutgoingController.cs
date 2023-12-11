using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmacy_Project.Infrastructure;
using Pharmacy_Project.Models;
using Pharmacy_Project.ViewModel;
using Pharmacy_Project.ViewModels;

namespace Pharmacy_Project.Controllers
{
    [Authorize]
    public class OutgoingController : Controller
    {
        private readonly PharmacyContext _context;
        private readonly int pageSize = 10;

        public OutgoingController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Outgoing
        public async Task<IActionResult> Index(int page = 1)
        {
            OutgoingsViewModel outgoingsModel = HttpContext.Session.Get<OutgoingsViewModel>("Outgoing");
            if (outgoingsModel == null)
                outgoingsModel = new OutgoingsViewModel();
            IQueryable<Outgoing> pharmacyContext = _context.Outgoings.Include(o => o.Medicine);
            pharmacyContext = OutgoingAvailability(pharmacyContext, outgoingsModel.DateStart, outgoingsModel.DateEnd);
            var count = pharmacyContext.Count();
            pharmacyContext = pharmacyContext.Skip((page - 1) * pageSize).Take(pageSize);

            outgoingsModel = new OutgoingsViewModel
            {
                Outgoings = pharmacyContext,
                Page = new PageViewModel(count, page, pageSize),
                DateStart = outgoingsModel.DateStart,
                DateEnd = outgoingsModel.DateEnd
            };
            return View(outgoingsModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(OutgoingsViewModel outgoingsViewModel)
        {
            HttpContext.Session.Set("Outgoing", outgoingsViewModel);
            return RedirectToAction("Index");
        }
        // GET: Outgoing/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Outgoings == null)
            {
                return NotFound();
            }

            var outgoing = await _context.Outgoings
                .Include(o => o.Medicine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outgoing == null)
            {
                return NotFound();
            }

            return View(outgoing);
        }

        // GET: Outgoing/Create
        public IActionResult Create()
        {
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name");
            return View();
        }

        // POST: Outgoing/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MedicineId,ImplementationDate,Count,SellingPrice")] Outgoing outgoing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(outgoing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", outgoing.MedicineId);
            return View(outgoing);
        }

        // GET: Outgoing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Outgoings == null)
            {
                return NotFound();
            }

            var outgoing = await _context.Outgoings.FindAsync(id);
            if (outgoing == null)
            {
                return NotFound();
            }
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", outgoing.MedicineId);
            return View(outgoing);
        }

        // POST: Outgoing/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedicineId,ImplementationDate,Count,SellingPrice")] Outgoing outgoing)
        {
            if (id != outgoing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outgoing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutgoingExists(outgoing.Id))
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
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", outgoing.MedicineId);
            return View(outgoing);
        }

        // GET: Outgoing/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Outgoings == null)
            {
                return NotFound();
            }

            var outgoing = await _context.Outgoings
                .Include(o => o.Medicine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outgoing == null)
            {
                return NotFound();
            }

            return View(outgoing);
        }

        // POST: Outgoing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Outgoings == null)
            {
                return Problem("Entity set 'PharmacyContext.Outgoings'  is null.");
            }
            var outgoing = await _context.Outgoings.FindAsync(id);
            if (outgoing != null)
            {
                _context.Outgoings.Remove(outgoing);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OutgoingExists(int id)
        {
            return (_context.Outgoings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public IQueryable<Outgoing> OutgoingAvailability(IQueryable<Outgoing> medicineAvailability, DateTime? selectedDateStart, DateTime? selectedDateEnd)
        {
            // Если дата не указана, используем текущую дату

            medicineAvailability = medicineAvailability
                .Include(m => m.Medicine)
                .Where(q => (q.ImplementationDate >= selectedDateStart && q.ImplementationDate <= selectedDateEnd) || (selectedDateStart == null && selectedDateEnd == null));

            return medicineAvailability;
        }
    }
}