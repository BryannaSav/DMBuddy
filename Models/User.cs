using System.Collections.Generic;

namespace DMBuddy.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        
        public string Username { get; set; }

        public string Password { get; set; }

        public List<Combat> MyCombats { get; set; }
        
        public User()
        {
            MyCombats = new List<Combat>();
        }
    }
}