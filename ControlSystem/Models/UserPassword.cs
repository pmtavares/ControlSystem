using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlSystem.Models
{

    [NotMapped]
    public class UserPassword : User
    {

        [Required(ErrorMessage = "The Field {0} é required!")]
        [StringLength(15, ErrorMessage = "The Field {0} can have a maximum {1} and minimum {2} caracteres", MinimumLength = 3)]
        public string Password { get; set; }
    }
}