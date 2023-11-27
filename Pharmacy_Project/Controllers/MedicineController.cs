using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class MedicineController : Controller
    {
        private readonly PharmacyContext _context;
        private readonly int pageSize = 10;

        public MedicineController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Medicine
        public async Task<IActionResult> Index(SortState sortOrder, int page = 1)
        {
            var medicine = HttpContext.Session.Get<MedicinesViewModel>("Medicine");
            if(medicine == null)
            {
                medicine = new MedicinesViewModel();
            }
            MedicinesViewModel medicinesModel;
            IQueryable<Medicine> pharmacyContext = _context.Medicines.Include(m => m.Producer);
            pharmacyContext = MedicineSort(pharmacyContext, sortOrder);
            var count = pharmacyContext.Count();
            pharmacyContext = pharmacyContext.Skip((page - 1) * pageSize).Take(pageSize);

            medicinesModel = new MedicinesViewModel
            {
                Medicines = pharmacyContext,
                SortViewModel = new SortViewModel(sortOrder),

                Page = new PageViewModel(count, page, pageSize),
                date = medicine.date
            };
            return View(medicinesModel);
        }
        public async Task<IActionResult> MedicineAvailability(SortState sortOrder, int page = 1)
        {
            var medicine = HttpContext.Session.Get<MedicinesViewModel>("Medicine");
            if (medicine == null)
            {
                medicine = new MedicinesViewModel();
            }
            MedicinesViewModel medicinesModel;
            IQueryable<Medicine> pharmacyContext = _context.Medicines.Include(m => m.Producer);
            pharmacyContext = MedicineSearch(pharmacyContext, sortOrder, medicine.date);
            var count = pharmacyContext.Count();
            pharmacyContext = pharmacyContext.Skip((page - 1) * pageSize).Take(pageSize);

            medicinesModel = new MedicinesViewModel
            {
                Medicines = pharmacyContext,
                SortViewModel = new SortViewModel(sortOrder),
                Page = new PageViewModel(count, page, pageSize),
                date = medicine.date
            };
            return View(medicinesModel);
        }
        [HttpPost]
        public IActionResult MedicineAvailability(MedicinesViewModel medicine)
        {

            HttpContext.Session.Set("Medicine", medicine);

            return RedirectToAction("MedicineAvailability");
        }
        // GET: Medicine/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medicines == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines
                .Include(m => m.Producer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        // GET: Medicine/Create
        public IActionResult Create()
        {
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Name");
            return View();
        }

        // POST: Medicine/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ShortDescription,ActiveSubstance,UnitMeasurement,Count,StorageLocation,ProducerId")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Name", medicine.ProducerId);
            return View(medicine);
        }

        // GET: Medicine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medicines == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Name", medicine.ProducerId);
            return View(medicine);
        }

        // POST: Medicine/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortDescription,ActiveSubstance,UnitMeasurement,Count,StorageLocation,ProducerId")] Medicine medicine)
        {
            if (id != medicine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicineExists(medicine.Id))
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
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Name", medicine.ProducerId);
            return View(medicine);
        }

        // GET: Medicine/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medicines == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines
                .Include(m => m.Producer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        // POST: Medicine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medicines == null)
            {
                return Problem("Entity set 'PharmacyContext.Medicines'  is null.");
            }
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine != null)
            {
                _context.Medicines.Remove(medicine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicineExists(int id)
        {
          return (_context.Medicines?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IQueryable<Medicine> MedicineSearch(IQueryable<Medicine> medicineAvailability, SortState sortOrder, DateTime? selectedDate)
        {
            switch (sortOrder)
            {
                case SortState.CountAsc:
                    medicineAvailability = medicineAvailability.OrderBy(s => s.Count);
                    break;
                case SortState.CountDesc:
                    medicineAvailability = medicineAvailability.OrderByDescending(s => s.Count);
                    break;
            }
            // Если дата не указана, используем текущую дату
            DateTime currentDate = selectedDate ?? new DateTime();

             medicineAvailability = medicineAvailability
                .Where(m => m.Incomings.Any(i => i.ArrivalDate.Date == currentDate.Date || currentDate.Date == new DateTime().Date
                || currentDate.Date == null))
                .Select(m => new Medicine
                {
                    Id = m.Id,
                    Name = m.Name,
                    Count = m.Incomings
                        .Where(i => i.ArrivalDate.Date == currentDate.Date)
                        .Sum(i => i.Count)
                });

            return medicineAvailability;
        }
        public IQueryable<Medicine> MedicineSort(IQueryable<Medicine> medicineAvailability, SortState sortOrder)
        {
            switch (sortOrder)
            {
                case SortState.CountAsc:
                    medicineAvailability = medicineAvailability.OrderBy(s => s.Count);
                    break;
                case SortState.CountDesc:
                    medicineAvailability = medicineAvailability.OrderByDescending(s => s.Count);
                    break;
            }
            return medicineAvailability;
        }
    }
}
