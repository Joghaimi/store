using Microsoft.AspNetCore.Mvc;
using Project1.Data;
using Project1.Models;
using System.Collections.Generic;

namespace Project1.Controllers
{
    public class ApplicationTypeController : Controller
    {
        ApplicationDbContext _db;
        public ApplicationTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationType> obj = _db.ApplicationType;
            return View(obj);
        }

        // GET - Create 
        public IActionResult Create()
        {
            return View();
        }
        // POST - Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType ObJ)
        {
            _db.ApplicationType.Add(ObJ);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}

