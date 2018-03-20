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
    public class SpellController : Controller
    {
        private GameContext _context;
 
        public SpellController(GameContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("AllSpells")]
        public IActionResult AllSpells(){
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }
            var SpellList = new Dictionary<string, object>();
            WebRequest.GetAllSpellDataAsync(ApiResponse =>
                {
                    SpellList = ApiResponse;
                }
            ).Wait();
            
            return View();
        }

        //IN WORK

        // [HttpGet]
        // [Route("spell/{spellid}")]
        // public IActionResult spell(int spellid){  
        //     var SpellInfo = new Dictionary<string, object>();
        //     WebRequest.GetSpellDataAsync(spellid, ApiResponse =>
        //         {
        //             SpellInfo = ApiResponse;
        //         }
        //     ).Wait();
        //     ViewBag.rollerToggle = HttpContext.Session.GetString("rollerToggle");
        //     ViewBag.spellInfo = SpellInfo;
        //     return View("AllSpells");

        // }

        [HttpPost]
        [Route("getSpell")]
        public IActionResult getSpell(string spellSearch){
        //IN WORK
            // var SpellList = new SpellAPIResponse();
            // WebRequest.GetAllSpellDataAsync(ApiResponse =>
            //     {
            //         SpellList = (SpellAPIResponse)ApiResponse;
            //     }
            // ).Wait();
            // System.Console.WriteLine(SpellList.results[0]);
            return View("AllSpells");
        }
    }
}
