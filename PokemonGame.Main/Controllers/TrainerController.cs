using PokemonGame.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace PokemonGame.Main.Controllers
{
    public class TrainerController : Controller
    {
        private readonly PokemonBDEntities _db = new PokemonBDEntities();

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError("", "User not found");
                return RedirectToAction("Index", "Home");
            }

            Team team = _db.Teams.SingleOrDefault(t => t.UserId == id);

            if (team == null)
            {
                ModelState.AddModelError("", "You don't have a team");

                return RedirectToAction("CreateTeam", "Trainer", id);
            }

            return View(team);
        }

        public ActionResult CreateTeam(int id)
        {
            

            return View();
        }
    }
}