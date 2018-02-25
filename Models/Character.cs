using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMBuddy.Models
{
    public class Character : BaseEntity
    {
        
        public int CharacterId { get; set; }
        
        [Required]
        [MinLength(2)]
        [RegularExpression("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$")]
        public string Name { get; set; }

        [Required]
        public int Initiative { get; set; }
        public int? HP { get; set; }
        public int? AC { get; set; }
        public int? PassivePerception { get; set; }

        [RegularExpression("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$")]
        public string Comments { get; set;}
        public int CombatId { get; set; }
        public Combat Combat { get; set; }
        List<AffectedCharacter> Conditions { get; set; }
        
        public Character()
        {
            Conditions = new List<AffectedCharacter>();
        }
    }
}