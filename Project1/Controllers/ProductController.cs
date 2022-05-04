using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;
using Project1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEvironment;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

            IEnumerable<Product> objList = _db.Product;
            foreach (var obj in objList)
            {
                obj.Catogery = _db.Catogery.FirstOrDefault(u => u.Id == obj.CatogeryId);
                obj.applicationType = _db.ApplicationType.FirstOrDefault(u => u.id == obj.ApplicationTypeId);
            }
            return View(objList);
        }

        // GET - UpSert 
        public IActionResult UpSert(int? id)
        {
            ProductVM productVM = new ProductVM();
            productVM.CategoryDropDown = _db.Catogery.Select(
                i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }
                );

            productVM.ApplicationTyptDropDown = _db.ApplicationType.Select(
           i => new SelectListItem
           {
               Text = i.Name,
               Value = i.id.ToString()
           }
           );



            if (id == null)
            {
                // Create New product
                productVM.prodct = new Product();
                return View(productVM);
            }
            else
            {
                // Edit Product 
                productVM.prodct = _db.Product.Find(id);
                if (productVM.prodct == null)
                    return NotFound();
                return View(productVM);

            }
            return View();
        }
        // POST - UpSert 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(ProductVM ObJ)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEvironment.WebRootPath;
                if (ObJ.prodct.Id == 0)
                {
                    // create new product
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    ObJ.prodct.Image = fileName + extension;
                    _db.Product.Add(ObJ.prodct);
                }
                else
                {
                    // Uplode
                    var objFromDb = _db.Product.AsNoTracking().FirstOrDefault(u => u.Id == ObJ.prodct.Id);
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);
                        var oldFile = Path.Combine(upload, objFromDb.Image);
                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        ObJ.prodct.Image = fileName + extension;
                    }
                    else
                    {
                        ObJ.prodct.Image = objFromDb.Image;
                    }
                    _db.Product.Update(ObJ.prodct);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(ObJ);

        }
        //// GET - Edit 
        //public IActionResult Edit(int? id)
        //{
        //    if (id == 0 || id == null)
        //        return NotFound();
        //    var obj = _db.Catogery.Find(id);
        //    if (obj == null)
        //        return NotFound();
        //    return View(obj);
        //}

        //// Post - Edit 
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Catogery ObJ)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Catogery.Update(ObJ);
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");

        //    }
        //    return View(ObJ);
        //}
        // GET - Edit 
        public IActionResult Delete(int? id)
        {
            //var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEvironment.WebRootPath;
            if (id == 0 || id == null)
                return NotFound();
            var obj = _db.Product.Find(id);
            if (obj == null)
                return NotFound();

            string upload = webRootPath + WC.ImagePath;
            var oldFile = Path.Combine(upload, obj.Image);
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }


            _db.Product.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
