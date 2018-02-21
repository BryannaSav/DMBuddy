using Microsoft.EntityFrameworkCore;
 
namespace DMBuddy.Models
{
    public class GameContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public GameContext(DbContextOptions<GameContext> options) : base(options) { }
        // public DbSet<User> Users { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<Combat> combat { get; set; }
        public DbSet<Character> character { get; set; }
        
    }
}