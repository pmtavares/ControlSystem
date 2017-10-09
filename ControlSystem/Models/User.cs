using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlSystem.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "The Field {0} é required!")]
        [StringLength(100, ErrorMessage = "The Field {0} can have a maximum {1} and minimum {2} caracteres", MinimumLength = 7)]
        [DataType(DataType.EmailAddress)]
        [Index("UserNameIndex", IsUnique = true)]
        public string UserName { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = " The Field {0} é required!")]
        [StringLength(50, ErrorMessage = " The Field {0} can have a maximum {1} and minimum {2} caracteres ", MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = " The Field {0} é required!")]
        [StringLength(50, ErrorMessage = " The Field {0} can have a maximum {1} and minimum {2} caracteres ", MinimumLength = 2)]
        public string Surname { get; set; }

        [Display(Name = "User")]
        public string Fullname { get { return string.Format("{0} {1}", this.Name, this.Surname); } }

        [Required(ErrorMessage = " The Field {0} é required!")]
        [StringLength(20, ErrorMessage = " The Field {0} can have a maximum {1} and minimum {2} caracteres ", MinimumLength = 7)]
        public string Phone { get; set; }

        [Required(ErrorMessage = " The Field {0} é required!")]
        [StringLength(100, ErrorMessage = " The Field {0} can have a  minimum {1} and  maximum {2} caracteres ", MinimumLength = 10)]
        public string Address { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name="Image")]
        public string Photo { get; set; }

        [Display(Name = "Student")]
        public bool Student { get; set; }

        [Display(Name = "Teacher")]
        public bool Teacher { get; set; }


        //[JsonIgnore] //Avoid serialization problems
        public virtual ICollection<Groups> Groups { get; set; }

        public virtual ICollection<GroupsDetails> GroupsDetails { get; set; }

    }
}