using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_Project.Models;

namespace Pharmacy_Project.Controllers
{
    [Authorize]
    public class ProducerController : Controller
    {
        private PharmacyContext db;

        public ProducerController(PharmacyContext context)
        {
            db = context;
        }

        [ResponseCache(CacheProfileName = "ModelCache")]
        public IActionResult ShowTable()
        {
            var pharmacyTypes = db.Producers.ToList();
            return View(pharmacyTypes);
        }
    }
}
