using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMBuddy.Models
{
    public class CombatViewModel
    {
        [Required]
        [MinLength(2)]
        [RegularExpression("^[A-Za-z0-9 _]*$")]
        [Display(Name="Name:  ")]
        public string CombatName { get; set; }
    
    }
}