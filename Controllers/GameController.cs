using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMBuddy.Models;
using Microsoft.AspNetCore.Http;

namespace DMBuddy.Controllers
{
    public class GameController : Controller
    {


        private GameContext _context;
 
        public GameController(GameContext context)
        {
            _context = context;
        }

        public IActionResult Games()
        {
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }
            List<Combat> myCombats = _context.combat.Where(fight => fight.UserId==(int)HttpContext.Session.GetInt32("CurId")).ToList();
            ViewBag.Combats = myCombats;
            return View();
        }

        public IActionResult Roller()
        {
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }
            return View();
        }

        [HttpPost]
        [Route("/CreateGame")]
        public IActionResult CreateGame(CombatViewModel game)
        {
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }
            if(ModelState.IsValid)
            {
                Combat NewCombat = new Combat{
                    CombatName = game.CombatName,
                    CurTurn = 0,
                    UserId = (int)HttpContext.Session.GetInt32("CurId")
                };
                _context.Add(NewCombat);
                _context.SaveChanges();
                return RedirectToAction("Games");
            }
            else{
                List<Combat> myCombats = _context.combat.Where(fight => fight.UserId==(int)HttpContext.Session.GetInt32("CurId")).ToList();
                ViewBag.Combats = myCombats;
                return View("Games");
            }
            
        }

        [HttpGet]
        [Route("DeleteGame/{id}")]
        public IActionResult DeleteGame(int id){
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }
            Combat curCombat = _context.combat.SingleOrDefault(fight => fight.CombatId == id);
            _context.combat.Remove(curCombat);
            _context.SaveChanges();
            return RedirectToAction("Games");
        }

        [HttpGet]
        [Route("ShowGame/{id}")]
        public IActionResult ShowGame(int id){
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }
            ViewBag.Id = id;
            Combat curCombat = _context.combat.Include(fight => fight.Characters).SingleOrDefault(fight => fight.CombatId == id);
            List<Character> charactersInit = curCombat.Characters.OrderByDescending(person => person.Initiative).ToList();
            ViewBag.curCombat = curCombat;
            ViewBag.charactersInit = charactersInit;
            return View("ShowGame");
        }

        


        public IActionResult Error()
        {
            return View();
        }

    }
}
