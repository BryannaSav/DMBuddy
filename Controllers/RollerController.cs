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
    public class RollerController : Controller
    {
        private GameContext _context;
 
        public RollerController(GameContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("DiceRoller")]
        public IActionResult DiceRoller(){
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }
            ViewBag.rollResult = TempData["rollResult"];
            ViewBag.rollerToggle = HttpContext.Session.GetString("rollerToggle");
            return View();
        }

        [HttpPost]
        [Route("rollClassicDice")]
        public IActionResult rollClassicDice(int diceQuanitiy, int diceType){
            Random rand = new Random();
            CryptoRandom rng = new CryptoRandom();
            int total = 0;
            for(var i=0; i<diceQuanitiy; i++){
                int num = rng.Next(1,diceType+1);
                total += num;
            }
            TempData["rollResult"] = total;
            return RedirectToAction("DiceRoller");
        }

        [HttpPost]
        [Route("rollCustomDice")]
        public IActionResult rollCustomDice(int diceQuanitiy, int diceType, int Modifier){
            Random rand = new Random();
            int total = 0;
            for(var i=0; i<diceQuanitiy; i++){
                int num = rand.Next(1,diceType+1);
                total += num;
                // System.Console.WriteLine(num);
            }
            total+=Modifier;
            TempData["rollResult"] = total;
            return RedirectToAction("DiceRoller");
        }

        [HttpGet]
        [Route("rollerToggle")]
        public IActionResult rollerToggle(){
            if(HttpContext.Session.GetString("rollerToggle")==null){
                HttpContext.Session.SetString("rollerToggle", "on");
            }else{
                HttpContext.Session.Remove("rollerToggle");
            }
            return RedirectToAction("DiceRoller");
        }

        [HttpGet]
        [Route("spell/{spellid}")]
        public IActionResult spell(int spellid){  
            var SpellInfo = new Dictionary<string, object>();
            WebRequest.GetSpellDataAsync(spellid, ApiResponse =>
                {
                    SpellInfo = ApiResponse;
                }
            ).Wait();
            ViewBag.rollerToggle = HttpContext.Session.GetString("rollerToggle");
            ViewBag.spellInfo = SpellInfo;
            return View("DiceRoller");

        }


        [HttpPost]
        [Route("getSpell")]
        public IActionResult getSpell(int spellid){
            return Redirect($"spell/{spellid}");

        }

        // [HttpPost]
        // [Route("RollDice")]
        // public IActionResult RollDice(){

        // }
    }
}
