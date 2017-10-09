using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlSystem.Models
{
    public class MyStudentResponse
    {

        public double Percentage { get; set; }
        public List<MyStudent> Student { get; set; }
    }
}