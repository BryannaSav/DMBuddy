using System.Collections.Generic;

namespace DMBuddy.Models
{
    public class Combat : BaseEntity
    {
        public int CombatId { get; set; }

        public int CurTurn { get; set; }

        public string CombatName { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Character> Characters { get; set; }
        public Combat()
        {
            Characters = new List<Character>();
        }
        
    }
}