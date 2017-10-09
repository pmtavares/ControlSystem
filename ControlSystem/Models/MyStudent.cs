using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlSystem.Models
{
    public class MyStudent
    {
        public int GroupsDetailsId { get; set; }
        public int GroupId { get; set; }
        public User Student { get; set; }
        public double score { get; set; }


    }
}