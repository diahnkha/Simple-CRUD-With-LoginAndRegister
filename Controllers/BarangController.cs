using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAS.Bootcamp.Mvc.Net.Models;
using RAS.Bootcamp.Mvc.Net.Models.Entities;
using RAS.Bootcamp.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RAS.Bootcamp.Mvc.Net.Controllers;

[Authorize(Roles = "PENJUAL")]

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
            var barangs = new BarangRequest();
            return View(barangs);
        }

    //POST
    [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BarangRequest obj)
        {
            var UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(UploadFolder))
                Directory.CreateDirectory(UploadFolder);

            var filename = $"{obj.Kode}{obj.FileImage.FileName}";
            var filePath = Path.Combine(UploadFolder, filename);
            
            using var stream = System.IO.File.Create(filePath);
            if(obj.FileImage != null)
            {
                obj.FileImage.CopyTo(stream);
            }

            var Url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/images/{filename}";

            _dbContext.Barangs.Add(new Barang
            {
                IdPenjual = 5,
                Kode = obj.Kode,
                Nama = obj.Nama,
                Harga = obj.Harga,
                Description = obj.Description,
                Filename = filename,
                Url = Url
            });

            //_dbContext.Barangs.Add(obj);
            _dbContext.SaveChanges();
            //TempData["success"] = "Category created successfully";
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


