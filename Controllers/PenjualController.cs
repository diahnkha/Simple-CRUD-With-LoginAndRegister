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
    public class PenjualController : Controller
    {
        // GET: /<controller>/

        private readonly AppDbContext _dbContext;
        private static List<Penjual> penjuals = new List<Penjual>();

        public PenjualController(ILogger<PenjualController> logger, AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Penjual> penjuals = _dbContext.Penjuals.ToList();
            return View(penjuals);
        }

        public IActionResult Create()
        {
            var penjuals = new Penjual();
            return View(penjuals);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Penjual obj)
        {
            _dbContext.Penjuals.Add(obj);
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
                var penjualFromDb = _dbContext.Penjuals.Find(id);
                //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
                //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

                if (penjualFromDb == null)
                {
                    return NotFound();
                }

                return View(penjualFromDb);
         }

            //POST
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(Penjual obj)
            {
                _dbContext.Penjuals.Update(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "Penjual updated successfully";
                return RedirectToAction("Index");
            }

            public IActionResult Delete(int? id)
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var penjualFromDb = _dbContext.Penjuals.Find(id);
                //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
                //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

                if (penjualFromDb == null)
                {
                    return NotFound();
                }

                return View(penjualFromDb);
            }

            //POST
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeletePOST(int? id)
            {
                var obj = _dbContext.Penjuals.Find(id);
                if (obj == null)
                {
                    return NotFound();
                }

                _dbContext.Penjuals.Remove(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "Penjual deleted successfully";
                return RedirectToAction("Index");

            }
        }
    }

