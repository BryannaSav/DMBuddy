using System.Collections.Generic;

namespace DMBuddy.Models
{
    public class Character : BaseEntity
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Initiative { get; set; }
        public int? HP { get; set; }
        public int? AC { get; set; }
        public int? PassivePerception { get; set; }
        public int CombatId { get; set; }
        public Combat Combat { get; set; }
        List<AffectedCharacter> Conditions { get; set; }
        
        public Character()
        {
            Conditions = new List<AffectedCharacter>();
        }
    }
}