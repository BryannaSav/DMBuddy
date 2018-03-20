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
        public IActionResult rollClassicDice(int diceQuanitiy, int diceType, int CombatId){
            CryptoRandom rng = new CryptoRandom();
            int total = 0;
            for(var i=0; i<diceQuanitiy; i++){
                int num = rng.Next(1,diceType+1);
                total += num;
            }
            TempData["rollResult"] = total;
            return RedirectToAction("ShowGame", "Game", new {id = CombatId});
        }

        [HttpPost]
        [Route("rollCustomDice")]
        public IActionResult rollCustomDice(int diceQuanitiy, int diceType, int Modifier, int CombatId){
            CryptoRandom rng = new CryptoRandom();
            int total = 0;
            for(var i=0; i<diceQuanitiy; i++){
                int num = rng.Next(1,diceType+1);
                total += num;
                // System.Console.WriteLine(num);
            }

            total+=Modifier;
            TempData["rollResult"] = total;
            return RedirectToAction("ShowGame", "Game", new {id = CombatId});
        }

        [HttpPost]
        [Route("rollDTwenty")]
        public IActionResult rollDTwenty(int CombatId){
            CryptoRandom rng = new CryptoRandom();
            int roll = rng.Next(1,21);
            TempData["rollResult"] = roll;
            return RedirectToAction("ShowGame", "Game", new {id = CombatId});
        }

        [HttpPost]
        [Route("rollerToggle")]
        public IActionResult rollerToggle(int CombatId){
            if(HttpContext.Session.GetString("rollerToggle")==null){
                HttpContext.Session.SetString("rollerToggle", "on");
            }else{
                HttpContext.Session.Remove("rollerToggle");
            }
            return RedirectToAction("ShowGame", "Game", new {id = CombatId});
        }

    }
}



//CODE TO PASTE IN FOR CHECKING RANDOM NUMBER GENERATION DISTRIBUTION
    //DECLARE OUTSIDE FOR LOOP
            // int one = 0;
            // int two = 0;
            // int three = 0;
            // int four = 0;
            // int five = 0;
            // int six =0;
            // int seven = 0;
            // int eight = 0;
            // int nine = 0;
            // int ten = 0;
            // int eleven = 0;
            // int twelve = 0;
            // int thirteen = 0;
            // int fouteen = 0;
            // int fifteen = 0;
            // int sixteen = 0;
            // int seventeen = 0;
            // int eighteen = 0;
            // int nineteen = 0;
            // int twenty = 0;
    //INCREASE COUNT INSIDE FOR LOOP
            //     if(num == 1){
            //         one++;
            //     }
            //     else if(num == 1){
            //         one++;
            //     }
            //     else if(num == 2){
            //         two++;
            //     }
            //     else if(num == 3){
            //         three++;
            //     }
            //     else if(num == 4){
            //         four++;
            //     }
            //     else if(num == 5){
            //         five++;
            //     }
            //     else if(num == 6){
            //         six++;
            //     }
            //     else if(num == 7){
            //         seven++;
            //     }
            //     else if(num == 8){
            //         eight++;
            //     }
            //     else if(num == 9){
            //         nine++;
            //     }
            //     else if(num == 10){
            //         ten++;
            //     }
            //     else if(num == 11){
            //         eleven++;
            //     }
            //     else if(num == 12){
            //         twelve++;
            //     }
            //     else if(num == 13){
            //         thirteen++;
            //     }
            //     else if(num == 14){
            //         fouteen++;
            //     }
            //     else if(num == 15){
            //         fifteen++;
            //     }
            //     else if(num == 16){
            //         sixteen++;
            //     }
            //     else if(num == 17){
            //         seventeen++;
            //     }
            //     else if(num == 18){
            //         eighteen++;
            //     }
            //     else if(num == 19){
            //         nineteen++;
            //     }
            //     else if(num == 20){
            //         twenty++;
            //     }
    //PRINT TOTALS AFTER FOR LOOP
            // System.Console.WriteLine($"one: {one}");
            // System.Console.WriteLine($"two: {two}");
            // System.Console.WriteLine($"three: {three}");
            // System.Console.WriteLine($"four: {four}");
            // System.Console.WriteLine($"five: {five}");
            // System.Console.WriteLine($"six: {six}");
            // System.Console.WriteLine($"seven: {seven}");
            // System.Console.WriteLine($"eight: {eight}");
            // System.Console.WriteLine($"nine: {nine}");
            // System.Console.WriteLine($"ten: {ten}");
            // System.Console.WriteLine($"eleven: {eleven}");
            // System.Console.WriteLine($"twelve: {twelve}");
            // System.Console.WriteLine($"thirteen: {thirteen}");
            // System.Console.WriteLine($"fouteen: {fouteen}");
            // System.Console.WriteLine($"fifteen: {fifteen}");
            // System.Console.WriteLine($"sixteen: {sixteen}");
            // System.Console.WriteLine($"seventeen: {seventeen}");
            // System.Console.WriteLine($"eighteen: {eighteen}");
            // System.Console.WriteLine($"nineteen: {nineteen}");
            // System.Console.WriteLine($"twenty: {twenty}");