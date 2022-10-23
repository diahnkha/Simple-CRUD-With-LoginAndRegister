using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAS.Bootcamp.Mvc.Net.Models;
using RAS.Bootcamp.Mvc.Net.Models.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RAS.Bootcamp.Mvc.Net.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class PembeliController : Controller
    {
        // GET: /<controller>/

        private readonly AppDbContext _dbContext;
        private static List<Pembeli> pembelies = new List<Pembeli>();

        public PembeliController(ILogger<PembeliController> logger, AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Pembeli> pembelies = _dbContext.Pembelies.ToList();
            return View(pembelies);
        }

        public IActionResult Create()
        {
            var pembelies = new Pembeli();
            return View(pembelies);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pembeli obj)
        {
            _dbContext.Pembelies.Add(obj);
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
            var pembeliFromDb = _dbContext.Pembelies.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (pembeliFromDb == null)
            {
                return NotFound();
            }

            return View(pembeliFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pembeli obj)
        {
            _dbContext.Pembelies.Update(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "Pembeli updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var pembeliFromDb = _dbContext.Pembelies.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (pembeliFromDb == null)
            {
                return NotFound();
            }

            return View(pembeliFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _dbContext.Pembelies.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _dbContext.Pembelies.Remove(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "Pembeli deleted successfully";
            return RedirectToAction("Index");

        }
    }
}

