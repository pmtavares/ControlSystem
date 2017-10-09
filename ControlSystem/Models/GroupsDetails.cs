using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlSystem.Models
{
    public class GroupsDetails
    {

        [Key]
        public int GroupsDetailsId { get; set; }

        public int GroupId { get; set; }

        public int UserId { get; set; }


        [JsonIgnore]
        public virtual Groups Groups { get; set; }

        [JsonIgnore]
        public virtual User Student { get; set; }

        public string GroupStudent { get { return string.Format("{0} / {1}", Groups.Description, Student.Fullname); } }

        //public virtual ICollection<Scores> Scores { get; set; }

    }
}