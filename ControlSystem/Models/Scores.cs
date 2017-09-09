using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlSystem.Models
{
    public class Scores
    {

        [Key]
        public int ScoreId { get; set; }

        public int GroupsDetailsId { get; set; }

        [Required(ErrorMessage = "Type the field {0}")]
        [Range(0, 1, ErrorMessage = "The field {0} have to be {1} and {2}")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public float Percentage { get; set; }

        [Required(ErrorMessage = "Type the field {0}")]
        [Range(0, 5, ErrorMessage = "The field {0} contains values between {1} and {2}")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public float Score { get; set; }

        public virtual GroupsDetails GroupsDetails { get; set; }

    }
}