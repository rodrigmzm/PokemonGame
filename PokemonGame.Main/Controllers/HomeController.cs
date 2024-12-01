using PokemonGame.Architecture.Helpers;
using PokemonGame.Data;
using PokemonGame.Main.Models;
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
            var query = _db.Users.SingleOrDefault(m => m.Email == user.Email && m.Password == user.Password);
            var role = _db.Users;

            if (query != null)
            {
                return View("~/Views/Trainer/Index.cshtml");
            }
            else
            {
                ModelState.AddModelError("", "The email or password are incorrect");
                return View("Index");
            }
        }  
        
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}