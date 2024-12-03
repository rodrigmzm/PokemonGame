using PokemonGame.Data;
using PokemonGame.Main.Models;
using PokemonGame.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

namespace PokemonGame.Main.Controllers
{
    public class HomeController : Controller
    {
        private readonly PokemonBDEntities _db = new PokemonBDEntities();

        public ActionResult Index()
        {
            return View();
        }

        //Register
        [HttpGet]
        public ActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
           if (ModelState.IsValid)
            {
                if(_db.Users.Any(x => x.Email == user.Email)) 
                {
                    ModelState.AddModelError("", "Email already register");
                    return View();
                }
                else
                {
                    var hashPassword = AccountService.HashPassword(user.Password);
                    user.Password = hashPassword;
                    user.IdRol = 2;
                    _db.Users.Add(user);
                    _db.SaveChanges();
                }
           }
           return View("Index");
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel user)
        {
            var hashPassword = AccountService.HashPassword(user.Password);
            var query = _db.Users.SingleOrDefault(x => x.Email == user.Email && x.Password == hashPassword);

            if (query != null)
            {
                if (query.IdRol == 1)
                {
                    return View("~/Views/Admin/Index.cshtml");
                }
                if (query.IdRol == 2)
                {
                    return View("~/Views/Trainer/Index.cshtml");
                }
                if (query.IdRol == 3)
                {
                    return View("~/Views/Nurse/Index.cshtml");
                }
            }
            ModelState.AddModelError("", "The email or password are incorrect");
            return View("Index");
        }  
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}