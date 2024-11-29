using PokemonGame.Architecture.Helpers;
using PokemonGame.Data;
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
        public ActionResult Register(string name, string email, string password)
        {
            if (_db.Users.Any(u => u.Email == email))
            {
                ModelState.AddModelError("", "Email already register");
                return View();
            }

            var hashedPassword = PasswordHelper.HashPassword(password);
            var newUser = new User
            {
                Name = name,
                Email = email,
                Password = hashedPassword,
                IdRol = 2
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        //Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null || !PasswordHelper.VerifyPassword(password, user.Password))
            {
                ModelState.AddModelError("", "Incorrect email or password.");
                return View("Index");
            }

            Session["UserId"] = user.UserId;
            Session["UserName"] = user.Email;

            if (user.IdRol == 1) return RedirectToAction("Index", "Admin");
            else if (user.IdRol == 2) return RedirectToAction("Index", "Trainer");
            else return RedirectToAction("Index", "Nurse");
        }  
        
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}