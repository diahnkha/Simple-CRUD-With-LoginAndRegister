using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RAS.Bootcamp.Mvc.Net.Models;
using RAS.Bootcamp.Mvc.Net.Models.Entities;
using RAS.Bootcamp.Net;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RAS.Bootcamp.Mvc.Net.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly AppDbContext _dbContext;

    public AccountController(ILogger<AccountController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Login()
    {
        return View(new LoginRequest());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var user = _dbContext.Users
        .FirstOrDefault(x => x.Username == request.Username && x.Password == request.Password);

        if (user == null)
        {
            ViewBag.ErrorMessage = "Invalid username or password";

            return View(request);
        }

        if (user.Tipe == "PEMBELI")
        {
            ViewBag.ErrorMessage = "You'r not admin or seller";

            return View(request);
        }

        //Set Authorization data to cookies
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("FullName", user.Username),
            new Claim(ClaimTypes.Role, user.Tipe),
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction("Login");
    }

    [HttpPost]
    public IActionResult Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var newUser = new Models.Entities.User
        {
            Username = request.Username,
            Password = request.Password,
            Tipe = request.Tipe
        };

        var penjual = new Models.Entities.Penjual
        {
            IdUser = newUser.Id,
            Alamat = request.Alamat,
            NamaToko = $"TK {request.Fullname}",
            User = newUser
        };

        _dbContext.Users.Add(newUser);
        _dbContext.Penjuals.Add(penjual);
        _dbContext.SaveChanges();

        return RedirectToAction("Login");
    }

    public IActionResult Register()
    {
        return View();
    }
}
