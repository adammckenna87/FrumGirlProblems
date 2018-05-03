using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrumGirlProblems.Models
{
    public class RegisterViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string email { get; set; }

        [System.ComponentModel.DataAnnotations.Required]

        public string password { get; set; }

    }
}
