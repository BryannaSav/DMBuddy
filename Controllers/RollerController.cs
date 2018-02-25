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
            return View();
        }

        // [HttpPost]
        // [Route("RollDice")]
        // public IActionResult RollDice(){

        // }
    }
}
