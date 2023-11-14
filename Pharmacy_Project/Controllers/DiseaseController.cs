using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy_Project.Models;

namespace Pharmacy_Project.Controllers
{
    [Authorize]
    public class DiseaseController : Controller
    {
        private PharmacyContext db;

        public DiseaseController(PharmacyContext context)
        {
            db = context;
        }

        [ResponseCache(CacheProfileName = "ModelCache")]
        public IActionResult ShowTable()
        {
            var pharmacyTypes = db.Diseases
                .Include(ia => ia.MedicinesForDiseases)
                .ToList();
            return View(pharmacyTypes);
        }
    }
}
