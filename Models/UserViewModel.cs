using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMBuddy.Models
{
    public class UserViewModel
    {

        [Required]
        [MinLength(2)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        [Display(Name="Username: ")]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        [Display(Name="Password: ")]
        // [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name="Confirm Password: ")]
        // [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}