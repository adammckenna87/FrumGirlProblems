using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrumGirlProblems.Models
{
    public class SignInViewModel
    {

        [System.ComponentModel.DataAnnotations.Required]
        public string email { get; set; }

        [System.ComponentModel.DataAnnotations.Required]

        public string password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
