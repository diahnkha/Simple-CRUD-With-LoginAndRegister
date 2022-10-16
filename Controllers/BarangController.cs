using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RAS.Bootcamp.Mvc.Net.Models;
using RAS.Bootcamp.Mvc.Net.Models.Entities;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RAS.Bootcamp.Mvc.Net.Controllers
{
    public class BarangController : Controller
    {
        private readonly AppDbContext _dbContext;
        private static List<Barang> barangs = new List<Barang>();

        public BarangController(ILogger<BarangController> logger, AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Barang> barangs = _dbContext.Barangs.ToList();
            return View(barangs);
        }

        public IActionResult Create()
        {
            var barangs = new Barang();
            return View(barangs);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Barang obj)
        {
                _dbContext.Barangs.Add(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var barangFromDb = _dbContext.Barangs.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (barangFromDb == null)
            {
                return NotFound();
            }

            return View(barangFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Barang obj)
        {
            _dbContext.Barangs.Update(obj);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var barangFromDb = _dbContext.Barangs.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (barangFromDb == null)
            {
                return NotFound();
            }

            return View(barangFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _dbContext.Barangs.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _dbContext.Barangs.Remove(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "Barang deleted successfully";
            return RedirectToAction("Index");

        }
    }
}

