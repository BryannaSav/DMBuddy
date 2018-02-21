using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMBuddy.Models
{
    public class CharacterViewModel
    {
        [Required]
        [MinLength(2)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string Name { get; set; }

        [Required]
        public int Initiative { get; set; }

        public int? HP { get; set; }

        public int? AC { get; set; }

        public int? PassivePerception { get; set; }

        public int CombatId { get; set; }
    }
}