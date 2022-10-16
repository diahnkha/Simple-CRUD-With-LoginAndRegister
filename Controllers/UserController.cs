using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using RAS.Bootcamp.Mvc.Net.Models;
using RAS.Bootcamp.Mvc.Net.Models.Entities;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RAS.Bootcamp.Mvc.Net.Controllers
{
    public class UserController : Controller
    {
        // GET: /<controller>/

        private readonly AppDbContext _dbContext;
        private static List<User> users = new List<User>();

        public UserController(ILogger<UserController> logger, AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<User> users = _dbContext.Users.ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            var users = new User();
            return View(users);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User obj)
        {
            if(obj.Id == null)
            {
                ModelState.AddModelError("CustomError", "The Id must filled");
            }
            if (ModelState.IsValid) {
                _dbContext.Users.Add(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userFromDb = _dbContext.Users.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User obj)
        {
            _dbContext.Users.Update(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "User updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userFromDb = _dbContext.Users.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _dbContext.Users.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "User deleted successfully";
            return RedirectToAction("Index");

        }


    }
}



