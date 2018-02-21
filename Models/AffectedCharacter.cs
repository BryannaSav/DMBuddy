namespace DMBuddy.Models
{
    public class AffectedCharacter : BaseEntity
    {
        public int AffectedCharacterId { get; set; }

        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public int ConditionId { get; set; }
        public Condition Condition { get; set; }

    }
}