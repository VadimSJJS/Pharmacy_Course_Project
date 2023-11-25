using System;
using System.Collections.Generic;
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
    public class ProducerController : Controller
    {
        private readonly PharmacyContext _context;
        private readonly int pageSize = 10;

        public ProducerController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Producer
        public async Task<IActionResult> Index(int page = 1)
        {
            ProducersViewModel producersModel;

            IQueryable<Producer> pharmacyContext = _context.Producers;

            var count = pharmacyContext.Count();
            pharmacyContext = pharmacyContext.Skip((page - 1) * pageSize).Take(pageSize);

            producersModel = new ProducersViewModel
            {
                Producers = pharmacyContext,
                Page = new PageViewModel(count, page, pageSize)

            };
            return View(producersModel);
        }

        // GET: Producer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Producers == null)
            {
                return NotFound();
            }

            var producer = await _context.Producers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // GET: Producer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Producers == null)
            {
                return NotFound();
            }

            var producer = await _context.Producers.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country")] Producer producer)
        {
            if (id != producer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducerExists(producer.Id))
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
            return View(producer);
        }

        // GET: Producer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Producers == null)
            {
                return NotFound();
            }

            var producer = await _context.Producers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Producer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Producers == null)
            {
                return Problem("Entity set 'PharmacyContext.Producers'  is null.");
            }
            var producer = await _context.Producers.FindAsync(id);
            if (producer != null)
            {
                _context.Producers.Remove(producer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(int id)
        {
          return (_context.Producers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
