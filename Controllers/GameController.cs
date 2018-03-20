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

        //GETTING GAMES METHODS ################################################################################

        [HttpGet]
        [Route("Games")]
        public IActionResult Games()
        {
            //LOGIN CHECK
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }

            //SETS CONFIRM DELETE GAME TOGGLE TO GAME'S ID IF DELETE BUTTON PRESSED
            if(TempData["ConfirmDeleteGame"] != null){
                ViewBag.ConfirmDeleteGame = TempData["ConfirmDeleteGame"];
            }

            if(TempData["GameUpdateToggle"] != null){
                ViewBag.GameUpdateToggle = TempData["GameUpdateToggle"];
                int comId = ViewBag.GameUpdateToggle;
                Combat uCombat = _context.combat.SingleOrDefault(fight => fight.CombatId == comId);
                ViewBag.uCombat = uCombat;
            }

            //GENERATES LIST OF ENCOUNTERS
            List<Combat> myCombats = _context.combat.Where(fight => fight.UserId==(int)HttpContext.Session.GetInt32("CurId")).ToList();
            ViewBag.Combats = myCombats;
            return View();
        }

        [HttpGet]
        [Route("ShowGame/{id}")]
        //THIS METHOD IS REFERENCED BY THE CHARACTERCONTROLLER AS WELL
        public IActionResult ShowGame(int id){
            //LOGIN CHECK
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }

            //FOR USE OF CHARACTERCONTROLLER SETS UP VIEWBAG DATA FOR UPDATE CHARACTER METHOD 
            if(TempData["UpdateToggle"]!=null){
                ViewBag.UpdateToggle = TempData["UpdateToggle"];
                int PlayerId = ViewBag.UpdateToggle;
                Character UpdateCharacter = _context.character.SingleOrDefault(player=>player.CharacterId == PlayerId);
                ViewBag.UpdateCharacter = UpdateCharacter;
            }

            //FOR USE OF CHARACTER CONTROLLER SETS UP VIEWBAG DATA FOR DELETE CHARACTER METHOD
            if(TempData["ConfirmDeleteCharacter"]!=null){
                ViewBag.ConfirmDeleteCharacter = TempData["ConfirmDeleteCharacter"];
            }

            //FOR USE OF DICE ROLLER TOGGLE AND DISPLAY MECHANICS
            ViewBag.rollResult = TempData["rollResult"];
            ViewBag.rollerToggle = HttpContext.Session.GetString("rollerToggle");

            //PAGE LOAD DB SETUP SAVED TO VIEWBAGS (would have used model binding if there weren't so many forms through partials)
            //--NOTE: come back to and see if viewbag info/query can be streamlined
            ViewBag.Id = id;
            Combat curCombat = _context.combat.Include(fight => fight.Characters).SingleOrDefault(fight => fight.CombatId == id);
            List<Character> charactersInit = curCombat.Characters.OrderByDescending(person => person.Initiative).ToList();
            ViewBag.curCombat = curCombat;
            ViewBag.charactersInit = charactersInit;
            ViewBag.numOfPlayers = charactersInit.Count();
            return View("ShowGame");
        }


        //CREATING GAMES METHODS ################################################################################

        [HttpPost]
        [Route("CreateGame")]
        public IActionResult CreateGame(CombatViewModel game)
        {
            //LOGIN CHECK
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }

            //GAME CREATION - IF INFO IS VALID
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

            //PAGE RENDER SETUP FOR IF INFO IS NOT VALID
            else{
                List<Combat> myCombats = _context.combat.Where(fight => fight.UserId==(int)HttpContext.Session.GetInt32("CurId")).ToList();
                ViewBag.Combats = myCombats;
                return View("Games");
            }
            
        }

        //DELETING GAMES METHODS ################################################################################

        [HttpGet]
        [Route("ConfirmDeleteGame/{CurGameId}")]
        public IActionResult ConfirmDeleteGame(int CurGameId){
            TempData["ConfirmDeleteGame"] = CurGameId;
            return RedirectToAction("Games");
        }

        [HttpGet]
        [Route("DeleteGame/{CurGameId}")]
        public IActionResult DeleteGame(int CurGameId){
            //LOGIN CHECK
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }
            //DELETION
            Combat curCombat = _context.combat.SingleOrDefault(fight => fight.CombatId == CurGameId);
            _context.combat.Remove(curCombat);
            _context.SaveChanges();
            return RedirectToAction("Games");
        }


        //UPDATING GAMES METHODS ################################################################################

        [HttpGet]
        [Route("ToggleUpdateGame/{CurGameId}")]
        public IActionResult ToggleUpdateGame(int CurGameId){
            TempData["GameUpdateToggle"] = CurGameId;
            return RedirectToAction("Games");
        }

        [HttpPost]
        [Route("UpdateGame")]
        public IActionResult UpdateGame(Combat updateInfo){
            //LOGIN CHECK
            if(HttpContext.Session.GetString("CurUser")==null) 
            {
                return RedirectToAction("LoginPage", "Login");
            }
            Combat oldGame = _context.combat.SingleOrDefault(fight=>fight.CombatId == updateInfo.CombatId);
            if(ModelState.IsValid){
                //PULLS COMBAT INFO UPDATES AND SAVES IF VALID
                oldGame.CombatName = updateInfo.CombatName;
                _context.SaveChanges();
                return RedirectToAction("Games");
            }else{
                //GENERATES LIST OF ENCOUNTERS IF NOT VALID
                List<Combat> myCombats = _context.combat.Where(fight => fight.UserId==(int)HttpContext.Session.GetInt32("CurId")).ToList();
                ViewBag.uCombat = oldGame;
                ViewBag.Combats = myCombats;
                ViewBag.GameUpdateToggle = updateInfo.CombatId;
                return View("Games");    
            }
        }

        [HttpGet]
        [Route("CancelChangeGame")]
        public IActionResult CancelChangeGame(){
            return RedirectToAction("Games");
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
