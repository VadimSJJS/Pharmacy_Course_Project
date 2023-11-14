using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy_Project.Models;

namespace Pharmacy_Project.Controllers
{
    [Authorize]
    public class MedicineForDiseaseController : Controller
    {
        private PharmacyContext db;

        public MedicineForDiseaseController(PharmacyContext context)
        {
            db = context;
        }

        [ResponseCache(CacheProfileName = "ModelCache")]
        public IActionResult ShowTable()
        {
            var pharmacyTypes = db.MedicinesForDiseases
                .Include(ia => ia.Disease)
                .Include(ia => ia.Midicine)
                .ToList();
            return View(pharmacyTypes);
        }
    }
}
