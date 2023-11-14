using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy_Project.Models;

namespace Pharmacy_Project.Controllers
{
    [Authorize]
    public class IncomingController : Controller
    {
        private PharmacyContext db;

        public IncomingController(PharmacyContext context)
        {
            db = context;
        }

        [ResponseCache(CacheProfileName = "ModelCache")]
        public IActionResult ShowTable()
        {
            var pharmacyTypes = db.Incomings
                .Include(ia => ia.Medicine)
                .ToList();
            return View(pharmacyTypes);
        }
    }
}
