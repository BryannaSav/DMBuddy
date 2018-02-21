using System.Collections.Generic;

namespace DMBuddy.Models
{
    public class Condition
    {
        public int ConditionId { get; set; }

        public string text { get; set; }

        public List<AffectedCharacter> AffectedCharacters { get; set; }

        public Condition()
        {
            AffectedCharacters = new List<AffectedCharacter>();
        }

    }
}