using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMBuddy.Models
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]   
        public DateTime UpdatedAt { get; set; }
        
    }



}