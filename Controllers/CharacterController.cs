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
    public class CharacterController : Controller
    {

        private GameContext _context;
 
        public CharacterController(GameContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AddCharacter")]
        public IActionResult AddCharacter(CharacterViewModel newCharacter){
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }
            if(ModelState.IsValid){
                Character myCharacter = new Character(){
                    Name = newCharacter.Name,
                    Initiative = newCharacter.Initiative,
                    HP = newCharacter.HP,
                    AC = newCharacter.AC,
                    PassivePerception = newCharacter.PassivePerception,
                    CombatId = newCharacter.CombatId
                };
                _context.character.Add(myCharacter);
                _context.SaveChanges();
            }
            Combat curCombat = _context.combat.Include(fight => fight.Characters).SingleOrDefault(fight => fight.CombatId == newCharacter.CombatId);
            List<Character> charactersInit = curCombat.Characters.OrderByDescending(person => person.Initiative).ToList();
            ViewBag.curCombat = curCombat;
            ViewBag.charactersInit = charactersInit;
            return RedirectToAction($"ShowGame", "Game", new {id = newCharacter.CombatId});
        }


    }

}