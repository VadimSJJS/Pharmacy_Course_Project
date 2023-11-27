using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Pharmacy_Project.Infrastructure;
using Pharmacy_Project.Models;
using Pharmacy_Project.ViewModel;
using Pharmacy_Project.ViewModels;
using X.PagedList;

namespace Pharmacy_Project.Controllers
{
    [Authorize]
    public class DiseaseController : Controller
    {
        private readonly PharmacyContext _context;
        private readonly int pageSize = 10;

        public DiseaseController(PharmacyContext context)
        {
            _context = context;
        }

        /*
        [ResponseCache(CacheProfileName = "ModelCache")]
        public IActionResult ShowTable()
        {
            var pharmacyTypes = _context.Diseases
                .Include(ia => ia.MedicinesForDiseases)
                .ToList();
            return View(pharmacyTypes);
        }
        */

        // GET: Disease
        public async Task<IActionResult> Index(int page = 1)
        {
            DiseasesViewModel diseasesModel;
            var disease = HttpContext.Session.Get<DiseasesViewModel>("Disease");
            if (disease == null)
            {
                disease = new DiseasesViewModel();
            }

            IQueryable<Disease> pharmacyContext = _context.Diseases;
            pharmacyContext = DiseaseAvailability(pharmacyContext, disease.DiseaseName);

            var count = pharmacyContext.Count();
            pharmacyContext = pharmacyContext.Skip((page - 1) * pageSize).Take(pageSize);

            diseasesModel = new DiseasesViewModel
            {
                Diseases = pharmacyContext,
                Page = new PageViewModel(count, page, pageSize),
                DiseaseName = disease.DiseaseName
            };

            return View(diseasesModel);
        }

        [HttpPost]
        public IActionResult Index(DiseasesViewModel disease)
        {

            HttpContext.Session.Set("Disease", disease);

            return RedirectToAction("Index");
        }

        // GET: Disease/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Diseases == null)
            {
                return NotFound();
            }

            var disease = await _context.Diseases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disease == null)
            {
                return NotFound();
            }

            return View(disease);
        }

        // GET: Disease/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Disease/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InternationalCode,Name")] Disease disease)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disease);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disease);
        }

        // GET: Disease/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Diseases == null)
            {
                return NotFound();
            }

            var disease = await _context.Diseases.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }
            return View(disease);
        }

        // POST: Disease/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InternationalCode,Name")] Disease disease)
        {
            if (id != disease.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disease);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiseaseExists(disease.Id))
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
            return View(disease);
        }

        // GET: Disease/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Diseases == null)
            {
                return NotFound();
            }

            var disease = await _context.Diseases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disease == null)
            {
                return NotFound();
            }

            return View(disease);
        }

        // POST: Disease/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Diseases == null)
            {
                return Problem("Entity set 'PharmacyContext.Diseases'  is null.");
            }
            var disease = await _context.Diseases.FindAsync(id);
            if (disease != null)
            {
                _context.Diseases.Remove(disease);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiseaseExists(int id)
        {
          return (_context.Diseases?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IQueryable<Disease> DiseaseAvailability(IQueryable<Disease> diseases,string diseaseName)
        {
            diseases = diseases
                .Where(e => e.Name.Contains(diseaseName ?? "")); 

            return diseases;
        }
    }
}
