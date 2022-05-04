using Microsoft.AspNetCore.Mvc;
using Project1.Data;
using Project1.Models;
using System.Collections.Generic;

namespace Project1.Controllers
{
    public class CatogeryController : Controller
    {

        private readonly ApplicationDbContext _db;
        public CatogeryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Catogery> objList = _db.Catogery;
            return View(objList);
        }

        // GET - Create 
        public IActionResult Create()
        {
            return View();
        }
        // POST - Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Catogery ObJ)
        {
            if (ModelState.IsValid)
            {
                _db.Catogery.Add(ObJ);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(ObJ);

        }
        // GET - Edit 
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();
            var obj = _db.Catogery.Find(id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        // Post - Edit 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Catogery ObJ)
        {
            if (ModelState.IsValid)
            {
                _db.Catogery.Update(ObJ);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(ObJ);
        }
        // GET - Edit 
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();
            var obj = _db.Catogery.Find(id);
            if (obj == null)
                return NotFound();
            
            _db.Catogery.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }





    }
}
