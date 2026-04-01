using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MojeTrasyController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MojeTrasyController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (email == null) return RedirectToAction("Login", "User");

            var zaznamy = _db.RideEntries
                .Where(r => r.UserEmail == email)
                .OrderByDescending(r => r.Datum)
                .ToList();

            return View(zaznamy);
        }

        [HttpGet]
        public IActionResult Pridat()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "User");

            return View();
        }

        [HttpPost]
        public IActionResult Pridat(RideEntry entry)
        {
            if (!ModelState.IsValid) return View(entry);

            entry.UserEmail = HttpContext.Session.GetString("UserEmail")!;
            entry.Datum = DateTime.Now;

            _db.RideEntries.Add(entry);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Smazat(int id)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var zaznam = _db.RideEntries
                .FirstOrDefault(r => r.Id == id && r.UserEmail == email);

            if (zaznam != null)
            {
                _db.RideEntries.Remove(zaznam);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}