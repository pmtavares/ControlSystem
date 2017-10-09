using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlSystem.Models
{
    public class Groups
    {

        [Key]
        public int GroupId { get; set; }

        [Required(ErrorMessage = " The field {0} is Required!")]
        [StringLength(50, ErrorMessage = " The field {0} must have a minimum of {1} and maximum of {2} characters ", MinimumLength = 3)]
        [Index("GroupoDescriptionIndex", IsUnique = true)]
        public string Description { get; set; }

        public int UserId { get; set; }


        [JsonIgnore] //Avoid serialization problems
        public virtual User Teacher { get; set; }


        [JsonIgnore]
        public virtual ICollection<GroupsDetails> GroupsDetails { get; set; }

    }
}