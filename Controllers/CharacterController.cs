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

        //CREATING CHARACTER METHODS ################################################################################

        [HttpPost]
        [Route("AddCharacter")]
        public IActionResult AddCharacter(Character newCharacter){
            //LOGIN CHECK
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }
            //CHARACTER CREATION - IF INFO IS VALID
            if(ModelState.IsValid){
                Character myCharacter = new Character(){
                    Name = newCharacter.Name,
                    Initiative = newCharacter.Initiative,
                    HP = newCharacter.HP,
                    AC = newCharacter.AC,
                    PassivePerception = newCharacter.PassivePerception,
                    CombatId = newCharacter.CombatId,
                    Comments = newCharacter.Comments
                };
                _context.character.Add(myCharacter);
                _context.SaveChanges();
            }

            //PAGE LOAD DB SETUP SAVED TO VIEWBAGS (would have used model binding if there weren't so many forms through partials)
            //--NOTE: come back to and see if viewbag info/query can be streamlined
            Combat curCombat = _context.combat.Include(fight => fight.Characters).SingleOrDefault(fight => fight.CombatId == newCharacter.CombatId);
            List<Character> charactersInit = curCombat.Characters.OrderByDescending(person => person.Initiative).ToList();
            ViewBag.curCombat = curCombat;
            ViewBag.charactersInit = charactersInit;
            ViewBag.numOfPlayers = charactersInit.Count();

            //PAGE RENDER SETUP FOR IF INFO IS NOT VALID
            if(!ModelState.IsValid){
                ViewBag.Id = newCharacter.CombatId;
                return View("ShowGame");
            }
            return RedirectToAction("ShowGame", "Game", new {id = newCharacter.CombatId});
        }


        //DELETING CHARACTER METHODS ################################################################################

        [HttpPost]
        [Route("ConfirmDeleteCharacter")]
        public IActionResult ConfirmDeleteCharacter(int CurCombatId, int CurPlayerId){
            TempData["ConfirmDeleteCharacter"] = CurPlayerId;
            return RedirectToAction("ShowGame", "Game", new {id = CurCombatId});
        }

        [HttpPost]
        [Route("DeleteCharacter")]
        public IActionResult DeleteCharacter(int CurCombatId, int CurPlayerId){
            Character CurCharacter = _context.character.SingleOrDefault(player=>player.CharacterId==CurPlayerId);
            _context.character.Remove(CurCharacter);
            _context.SaveChanges();
            return RedirectToAction("ShowGame", "Game", new {id = CurCombatId});
        }


        //UPDATTING CHARACTER METHODS ################################################################################
        
        
        [HttpPost]
        [Route("ToggleUpdateCharacter")]
        //MAKES UPDATE/CANCEL BUTTONS APPEAR, REMOVES CREATE FORM UNTIL UPDATE IS COMPLETE
        public IActionResult ToggleUpdateCharacter(int CurCombatId, int CurPlayerId){
            
            TempData["UpdateToggle"]=CurPlayerId;
            return RedirectToAction("ShowGame", "Game", new {id = CurCombatId});
        }

        [HttpPost]
        [Route("UpdateCharacter")]
        public IActionResult UpdateCharacter(Character updateInfo){
            //LOGIN CHECK
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }
            
            //UPDATES CHARACTER IF INFO IS VALID
            if(ModelState.IsValid){
                Character oldCharacter = _context.character.SingleOrDefault(player=>player.CharacterId==updateInfo.CharacterId);
                oldCharacter.AC = updateInfo.AC;
                oldCharacter.HP = updateInfo.HP;
                oldCharacter.Initiative = updateInfo.Initiative;
                oldCharacter.Name = updateInfo.Name;
                oldCharacter.PassivePerception = updateInfo.PassivePerception;
                oldCharacter.Comments = updateInfo.Comments;
                _context.SaveChanges();
            }

            //PAGE LOAD DB SETUP SAVED TO VIEWBAGS (would have used model binding if there weren't so many forms through partials)
            //--NOTE: come back to and see if viewbag info/query can be streamlined
            Combat curCombat = _context.combat.Include(fight => fight.Characters).SingleOrDefault(fight => fight.CombatId == updateInfo.CombatId);
            List<Character> charactersInit = curCombat.Characters.OrderByDescending(person => person.Initiative).ToList();
            ViewBag.curCombat = curCombat;
            ViewBag.charactersInit = charactersInit;
            ViewBag.numOfPlayers = charactersInit.Count();

            //PAGE RENDER SETUP FOR IF INFO IS NOT VALID 
            if(!ModelState.IsValid){
                ViewBag.UpdateCharacter = updateInfo;
                ViewBag.UpdateToggle = updateInfo.CharacterId;
                return View("ShowGame");
            }
            return RedirectToAction("ShowGame", "Game", new {id = updateInfo.CombatId});
        }

        [HttpGet]
        [Route("CancelUpdate/{curCombatId}")]
        //REMOVES UPDATE/CANCEL BUTTONS AND BRINGS BACK CREATE FORM
        public IActionResult CancelUpdate(int CurCombatId){
            // System.Console.WriteLine(CurCombatId);
            return RedirectToAction("ShowGame", "Game", new {id = CurCombatId});
        }

        [HttpPost]
        [Route("NextTurn")]
        public IActionResult NextTurn(int damage, int heals, int CharacterId, int CombatId){
            Character curUser = _context.character.SingleOrDefault(player => player.CharacterId==CharacterId);
            if(curUser.HP != null){
                curUser.HP -= damage;
                curUser.HP += heals;
                if(curUser.HP < 0){
                    curUser.HP = 0;
                }
            }
            Combat curCombat = _context.combat.Include(fight => fight.Characters).SingleOrDefault(fight => fight.CombatId == CombatId);
            int max = (curCombat.Characters.Count() - 1);
            if(curCombat.CurTurn == max){
                curCombat.CurTurn=0;
            }else{
                curCombat.CurTurn++;
            }
            _context.SaveChanges();
            return RedirectToAction("ShowGame", "Game", new {id = CombatId});
        }


    }

}